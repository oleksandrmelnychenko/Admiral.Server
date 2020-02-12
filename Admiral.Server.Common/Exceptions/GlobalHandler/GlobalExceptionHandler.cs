using Admiral.Server.Common.Exceptions.GlobalHandler.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Admiral.Server.Common.Exceptions.GlobalHandler {
    public class GlobalExceptionHandler : IGlobalExceptionHandler {
        /// <summary>
        ///     Handle all kind of exceptions ( Server, User, etc. )
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exceptionHandlerFeature"></param>
        /// <param name="isDevelopmentMode"></param>
        /// <returns></returns>
        public async Task HandleException(HttpContext httpContext, IExceptionHandlerFeature exceptionHandlerFeature, bool isDevelopmentMode) {
            //Unhandler sever exceptions.
            await HandleServerException(httpContext, exceptionHandlerFeature, isDevelopmentMode);
        }

        private async Task HandleServerException(HttpContext context, IExceptionHandlerFeature exceptionHandler, bool isDevelopment) {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";

            string developerMessage = string.Format(CultureInfo.CurrentCulture,
                $"{exceptionHandler.Error.Message}</br>{exceptionHandler.Error.InnerException}</br>{exceptionHandler.Error.StackTrace}");

            if (isDevelopment) {
                await context.Response.WriteAsync(developerMessage).ConfigureAwait(false);
            }

            Log.Fatal(developerMessage);
        }
    }
}
