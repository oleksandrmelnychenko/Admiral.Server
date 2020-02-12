using Admiral.Server.Common;
using Admiral.Server.Common.Exceptions.GlobalHandler.Contracts;
using Admiral.Server.Common.IdentityConfiguration;
using Admiral.Server.Databases;
using Admiral.Server.Domain.DataSourceAdapters.SQL;
using Admiral.Server.Domain.DataSourceAdapters.SQL.Contracts;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Admiral.Server.Common.Exceptions.GlobalHandler;
using Microsoft.AspNetCore.Http;
using Admiral.Server.Common.ResponseBuilder;
using Admiral.Server.Domain.Repositories.Identity;
using Admiral.Server.Common.ResponseBuilder.Contracts;
using Admiral.Server.Domain.Repositories.Identity.Contracts;
using Admiral.Server.Services.IdentityServices;
using Admiral.Server.Services.IdentityServices.Contracts;
using Admiral.Server.Domain.DbConnectionFactory;

namespace Admiral.Server.Core {
    public class Startup {
        private readonly IHostingEnvironment _environment;

        public IConfiguration Configuration { get; private set; }

        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env) {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

#if DEBUG
            env.EnvironmentName = ProductEnvironment.Development;
#elif RELEASE
            env.EnvironmentName = ProductEnvironment.Production;
#endif


            builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

            ConfigurationManager.SetAppSettingsProperties(Configuration);

            ConfigurationManager.SetContentRootDirectoryPath(env.ContentRootPath);

            _environment = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            ConfigurateDbConext(services);

            services.AddResponseCompression();
            services.AddCors();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            ConfigureJwtAuthService(services);

            //services.AddControllers();
            //    .AddNewtonsoftJson(options => {
            //        // Use the default property (Pascal) casing
            //        options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            //        // Configure a custom converter
            //        options.SerializerOptions.Converters.Add(new MyCustomJsonConverter());
            //    });

            services.AddMvc(options => {
                options.CacheProfiles.Add(CacheControlProfiles.Default,
                    new CacheProfile() {
                        Duration = 60
                    });
                options.CacheProfiles.Add(CacheControlProfiles.TwoHours,
                    new CacheProfile() {
                        Duration = 7200
                    });
                options.CacheProfiles.Add(CacheControlProfiles.HalfDay,
                    new CacheProfile() {
                        Duration = 43200
                    });
                options.EnableEndpointRouting = false;
            }).AddXmlSerializerFormatters()
                    //.AddJsonOptions(options => {
                    //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                    //    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    //    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    //    //options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                    //})
                    ;

            ApplicationContainer = InitAppServices(services);

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IGlobalExceptionFactory globalExceptionFactory) {
            IApplicationLifetime appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            InitLogger();

            app.UseResponseCompression();

            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions {
                RequestPath = "/Exports",
                FileProvider = new PhysicalFileProvider(_environment.ContentRootPath + "\\exports")
            })
                .UseHttpsRedirection()
                .UseCookiePolicy()
                .UseAuthentication()
                .UseResponseCompression();

            app.UseAuthentication();

            app.UseExceptionHandler(builder => {
                builder.Run(
                    async context => {
                        IExceptionHandlerFeature error = context.Features.Get<IExceptionHandlerFeature>();
                        IGlobalExceptionHandler globalExceptionHandler = globalExceptionFactory.New();

                        await globalExceptionHandler.HandleException(context, error, _environment.IsDevelopment());
                    });
            });

            app.UseMvcWithDefaultRoute();

            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }

        private void InitLogger() {
            LoggerConfiguration logger = new LoggerConfiguration();
            logger.Enrich.FromLogContext().MinimumLevel.Information().WriteTo.File
                (new JsonFormatter(), Path.Combine(ConfigurationManager.LogDirectoryPath, "devInfo.json"));

            Log.Logger = logger.CreateLogger();
        }

        private void ConfigurateDbConext(IServiceCollection services) {
            string connectionString;

#if DEBUG
            connectionString = Configuration.GetConnectionString(ConnectionStringNames.DefaultConnection);
#elif RELEASE
           connectionString = Configuration.GetConnectionString(ConnectionStringNames.ProductionConnection);
#endif

            services.AddDbContext<AdmiralDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(p => new AdmiralDbContext(p.GetService<DbContextOptions<AdmiralDbContext>>()));
        }

        private void ConfigureJwtAuthService(IServiceCollection services) {
            SymmetricSecurityKey signingKey = AuthOptions.GetSymmetricSecurityKey(ConfigurationManager.AppSettings.TokenSecret);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,

                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE_LOCAL,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents() {
                    OnMessageReceived = context => {
                        StringValues accessToken = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private IContainer InitAppServices(IServiceCollection services) {
            services.Add(new ServiceDescriptor(typeof(ISqlDbContext),
                t => new SqlDbContext(new AdmiralDbContext(t.GetService<DbContextOptions<AdmiralDbContext>>())), ServiceLifetime.Transient)
            );

            ContainerBuilder builder = new ContainerBuilder();

            builder.Populate(services);

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterType<GlobalExceptionHandler>().As<IGlobalExceptionHandler>();
            builder.RegisterType<GlobalExceptionFactory>().As<IGlobalExceptionFactory>();

            #region Repositories

            builder.RegisterType<IdentityRepository>().As<IIdentityRepository>();

            #endregion

            #region Factories

            builder.RegisterType<ResponseFactory>().As<IResponseFactory>();
            builder.RegisterType<IdentityRepositoriesFactory>().As<IIdentityRepositoriesFactory>();

            builder.RegisterType<DbConnectionFactory>().As<IDbConnectionFactory>();
            builder.RegisterType<SqlDbContext>().As<ISqlDbContext>();
            builder.RegisterType<SqlContextFactory>().As<ISqlContextFactory>();

            #endregion

            #region Services

            builder.RegisterType<UserIdentityService>().As<IUserIdentityService>();

            #endregion

            return builder.Build();
        }
    }
}
