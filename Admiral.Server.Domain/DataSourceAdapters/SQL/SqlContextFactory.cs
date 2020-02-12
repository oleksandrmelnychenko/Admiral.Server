using Admiral.Server.Domain.DataSourceAdapters.SQL.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Admiral.Server.Domain.DataSourceAdapters.SQL {
    public class SqlContextFactory : ISqlContextFactory {
        private readonly ISqlDbContext _sqlDbContext;

        public SqlContextFactory([FromServices] ISqlDbContext sqlDbContext) {
            _sqlDbContext = sqlDbContext;
        }

        public ISqlDbContext New() {
            return _sqlDbContext;
        }
    }
}
