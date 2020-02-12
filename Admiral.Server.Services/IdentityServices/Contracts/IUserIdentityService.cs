using Admiral.Server.Domain.DataContracts;
using Admiral.Server.Domain.Entities.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admiral.Server.Services.IdentityServices.Contracts {

    public interface IUserIdentityService {

        Task<UserAccount> SignInAsync(AuthenticationDataContract authenticateDataContract);


        Task<UserAccount> ValidateToken(ClaimsPrincipal userPrincipal);

        Task<UserAccount> NewUser(NewUserDataContract newUserDataContract);
    }
}