using Admiral.Server.Domain.Entities.Identity;
using System;

namespace Admiral.Server.Domain.Repositories.Identity.Contracts {
    public interface IIdentityRepository {
        int UpdateUserPassword(UserIdentity user);

        bool IsEmailAvailable(string email);

        void UpdateUserLastLoggedInDate(long userId, DateTime current);

        void UpdateUserExperationDate(long userId, bool isExpired);

        UserIdentity GetUserByEmail(string email);

        UserIdentity GetUserById(long userId);

        UserIdentity NewUser(string name, string description, string email, string passwordHash, string passwordSalt);

        long NewUser(UserIdentity userIdentity);

        UserAccount GetAccountByUserId(long userId);
    }
}
