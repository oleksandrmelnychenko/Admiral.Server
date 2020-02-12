using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Admiral.Server.Common.IdentityConfiguration {
    /// <summary>
    /// Provides helper methods for encrypting and decrypting strings
    /// </summary>
    public static class CryptoHelper {
        /// <summary>
        /// Create a random salt.
        /// </summary>
        /// <remarks>See https://en.wikipedia.org/wiki/Salt_(cryptography) for more information.</remarks>
        /// <returns>A salt value.</returns>
        public static string CreateSalt() {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create()) {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        /// <summary>
        /// Hashes a string value with the provided salt.
        /// </summary>
        /// <param name="value">The string value to hash.</param>
        /// <param name="salt">A unique salt value.</param>
        /// <returns>A hashed value.</returns>
        /// <remarks>The salt value can be provided using the <see cref="CreateSalt"/> method.</remarks>
        public static string Hash(
            string value,
            string salt) {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        /// <summary>
        /// Compares a provided value against a salt and hash to determine if the value matches.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <param name="salt">The salt used to generate the hash.</param>
        /// <param name="hash">The hash created from the original value.</param>
        /// <returns>TRUE if the value matches the provided salt and hash.</returns>
        /// <remarks>
        /// <para>For password storage, it is recommended that a salt and hash value be generated when a password is created and the
        /// salt and hash stored within the database instead of the raw password. Then using the method below, when a user tries to
        /// validate a provided password, the salt and hash are read from the database and the provided value is then salted with the
        /// stored salt and hashed together with the provided value. The password is deemed correct if the resulting hash matches the
        /// hash stored within the database.</para>
        /// <para>The above has a number of advantages, including:</para>
        /// <list type="bullet">
        /// <item>The raw password is never stored anywhere.</item>
        /// <item>Salted and hashed passwords guard against dictionary and rainbow table attacks.</item>
        /// <item>Small passwords are stretched as the salt and hash values are all the same length independent of the provided value
        /// length making brute force attacks extremely time consuming.</item>
        /// </list>
        /// <para>See https://en.wikipedia.org/wiki/Salt_(cryptography) for more information.</para>
        /// </remarks>
        public static bool Validate(
            string value,
            string salt,
            string hash) => Hash(value, salt) == hash;
    }
}
