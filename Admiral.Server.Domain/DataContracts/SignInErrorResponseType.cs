using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Domain.DataContracts
{
    public enum SignInErrorResponseType
    {
        InvalidEmail,
        InvalidCredentials,
        PasswordExpired,
        NotAllowed,
        InvalidToken,
        TokenExpired,
        UserDeleted
    }
}
