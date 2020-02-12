using Admiral.Server.Domain.DataSourceAdapters.SQL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Admiral.Server.Domain.DataSourceAdapters.SQL {
    public class SqlDbContext : ISqlDbContext {
        public DbContext DbContext { get; }

        public SqlDbContext(DbContext dbContext) {
            DbContext = dbContext;
        }
    }
}
