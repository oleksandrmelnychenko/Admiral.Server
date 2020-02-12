namespace Admiral.Server.Common.IdentityConfiguration {
    public class IdentityValidationMessages {
        public const string USER_NOT_SPECIFIED = "User not specified";

        public const string USER_NOT_EXISTS = "User not exists";

        public const string USER_DELETED = "User deleted";

        public const string EMAIL_INVALID = "Email is not valid";

        public const string EMAIL_NOT_AVAILABLE = "Email is not available";

        public const string TOKEN_INVALID = "Invalid token";

        public const string TOKEN_EXPIRED = "Authorization token expired";

        public const string NOT_ALLOWED = "Not allowed";

        public const string PASSWORD_EXPIRED = "Your password has expired, please update it now";

        public const string PASSWORD_EXPIRED_PLEASE_RESET = "Your password has expired. Please contact the project administrator to reset it for you.";

        public const string INVALID_CREDENTIALS = "Email or password did not match the user credentials";

        public const string USER_NOT_ALLOW_TO_RESET_PASSWORD = "User is not allowed to reset an expired password";

        public const string PASSWORD_MUST_BE_DIFFERENT = "Password must be different to old password";

        public const string MORE_THAN_ONE_GLOBAL_ROLE = "User can not have more than one global role";
    }
}
