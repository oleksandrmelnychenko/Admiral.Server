using System.Data;

namespace Admiral.Server.Domain.DbConnectionFactory {
    public interface IDbConnectionFactory {
        IDbConnection NewSqlConnection();
    }
}
