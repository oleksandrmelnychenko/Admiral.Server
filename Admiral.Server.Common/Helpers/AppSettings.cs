namespace Admiral.Server.Common.Helpers {
    public class AppSettings {
        /// <summary>
        /// Number of days after setting a password before the password expires.
        /// </summary>
        public int PasswordExpiryDays { get; set; }

        /// <summary>
        /// A regular expression used to determine the minimum password strength.
        /// </summary>
        /// <remarks>
        /// The following regular expression ensures that the password is:
        /// <list type="bullet">
        /// <item>At least 6 characters long</item>
        /// <item>Is no longer than 18 characters</item>
        /// <item>Contains at least one digit</item>
        /// <item>Contains at least one lower case character</item>
        /// <item>Contains at least one upper case character</item>
        /// </list>
        /// <code>^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,18}$</code>
        /// </remarks>
        public string PasswordStrongRegex { get; set; }

        /// <summary>
        /// Default message reported from the API if the password was not strong enough.
        /// </summary>
        /// <remarks>
        /// The message is configurable as it should specify the minimum password strength requirements to report back to the user.
        /// </remarks>
        public string PasswordWeakErrorMessage { get; set; }

        /// <summary>
        /// Number of days that a JWT token is valid. Users will need to re-authenticate to get another token once the token has expired.
        /// </summary>
        public int TokenExpiryDays { get; set; }

        /// <summary>
        /// Secret key used to encrypt the JWT Token. Ensure this is long and difficult to guess.
        /// </summary>
        public string TokenSecret { get; set; }
    }
}
