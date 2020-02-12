using Admiral.Server.Common.Helpers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Admiral.Server.Common {
    public class ConfigurationManager {
        private static string _databaseConnectionString;

        private static string _rootDirectoryPath;

        private static AppSettings _appSettings;

        public static string DatabaseConnectionString => _databaseConnectionString;

        public static AppSettings AppSettings => _appSettings;

        public static string ContentRootPath => _rootDirectoryPath;

        public static string LogDirectoryPath => Path.Combine(_rootDirectoryPath, "Logs");

        public static string ExportDirectoryPath => Path.Combine(_rootDirectoryPath, "Exports");

        public static void SetAppSettingsProperties(IConfiguration configuration) {

#if DEBUG
            _databaseConnectionString = configuration.GetConnectionString(ConnectionStringNames.DefaultConnection);
#elif RELEASE
           _databaseConnectionString = configuration.GetConnectionString(ConnectionStringNames.ProductionConnection);
#endif
            AppSettings appSettings = new AppSettings {
                TokenSecret = configuration.GetSection("ApplicationSettings")["TokenSecret"],
                TokenExpiryDays = int.Parse(configuration.GetSection("ApplicationSettings")["TokenExpiryDays"]),
                PasswordWeakErrorMessage =
                    configuration.GetSection("ApplicationSettings")["PasswordWeakErrorMessage"],
                PasswordStrongRegex = configuration.GetSection("ApplicationSettings")["PasswordStrongRegex"],
                PasswordExpiryDays =
                    int.Parse(configuration.GetSection("ApplicationSettings")["PasswordExpiryDays"]),
            };

            _appSettings = appSettings;
        }

        public static void SetContentRootDirectoryPath(string path) {
            _rootDirectoryPath = path;

            if (!Directory.Exists(ExportDirectoryPath)) Directory.CreateDirectory(ExportDirectoryPath);
        }
    }
}
