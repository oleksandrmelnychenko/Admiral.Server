using Admiral.Server.Common;
using System.Data;
using System.Data.SqlClient;

namespace Admiral.Server.Domain.DbConnectionFactory {
    public class DbConnectionFactory : IDbConnectionFactory {
        public IDbConnection NewSqlConnection() {
            return new SqlConnection(ConfigurationManager.DatabaseConnectionString);
        }
    }
}
