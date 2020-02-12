using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Common.IdentityConfiguration {
    public class AuthOptions {
        public const string ISSUER = "Admiral";

        public const string AUDIENCE_LOCAL = "http://localhost:62202/";
        public const string AUDIENCE_REMOTE = "http://localhost:62202/";

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
