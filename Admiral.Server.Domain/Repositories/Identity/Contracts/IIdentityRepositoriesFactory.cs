using System.Data;

namespace Admiral.Server.Domain.Repositories.Identity.Contracts {
    public interface IIdentityRepositoriesFactory {
        IIdentityRepository NewIdentityRepository(IDbConnection connection);
    }
}
