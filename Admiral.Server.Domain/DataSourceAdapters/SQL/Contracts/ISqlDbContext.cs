using Microsoft.EntityFrameworkCore;

namespace Admiral.Server.Domain.DataSourceAdapters.SQL.Contracts {
    public interface ISqlDbContext {
        DbContext DbContext { get; }
    }
}
