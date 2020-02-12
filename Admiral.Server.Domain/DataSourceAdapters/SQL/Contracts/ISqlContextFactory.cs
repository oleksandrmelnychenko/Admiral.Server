namespace Admiral.Server.Domain.DataSourceAdapters.SQL.Contracts {
    public interface ISqlContextFactory {
        ISqlDbContext New();
    }
}
