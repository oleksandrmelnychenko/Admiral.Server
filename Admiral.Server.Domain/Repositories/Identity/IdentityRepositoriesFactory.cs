using Admiral.Server.Domain.Repositories.Identity.Contracts;
using System.Data;

namespace Admiral.Server.Domain.Repositories.Identity {
    public class IdentityRepositoriesFactory : IIdentityRepositoriesFactory {
        public IIdentityRepository NewIdentityRepository(IDbConnection connection) =>
            new IdentityRepository(connection);
    }
}
