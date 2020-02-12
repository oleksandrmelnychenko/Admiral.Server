using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Common.IdentityConfiguration {
    /// <summary>
    ///     Provides helper methods for generating strong password
    /// </summary>
    public static class PasswordGenerationHelper {

        //Defined default charsets for password per charset type
        static readonly string AlphaCaps = "QWERTYUIOPASDFGHJKLZXCVBNM";
        static readonly string AlphaLow = "qwertyuiopasdfghjklzxcvbnm";
        static readonly string Numerics = "1234567890";
        static readonly string Special = "@#$_!-";

        static readonly string allChars = AlphaCaps + AlphaLow + Numerics + Special;

        static readonly Random r = new Random();

        public static string GenerateStrongPassword(int length) {
            StringBuilder generatedPassword = new StringBuilder();

            int pLower, pUpper, pNumber, pSpecial;

            string posArray = "0123456789";

            if (length < posArray.Length)
                posArray = posArray.Substring(0, length);

            pLower = GetRandomPosition(ref posArray);
            pUpper = GetRandomPosition(ref posArray);
            pNumber = GetRandomPosition(ref posArray);
            pSpecial = GetRandomPosition(ref posArray);

            for (int i = 0; i < length; i++) {
                if (i.Equals(pLower))
                    generatedPassword.Append(GetRandomChar(AlphaCaps));
                else if (i.Equals(pUpper))
                    generatedPassword.Append(GetRandomChar(AlphaLow));
                else if (i.Equals(pNumber))
                    generatedPassword.Append(GetRandomChar(Numerics));
                else if (i.Equals(pSpecial))
                    generatedPassword.Append(GetRandomChar(Special));
                else
                    generatedPassword.Append(GetRandomChar(allChars));
            }

            return generatedPassword.ToString();
        }

        private static string GetRandomChar(string fullString) {
            return fullString.ToCharArray()[(int)Math.Floor(r.NextDouble() * fullString.Length)].ToString();
        }

        private static int GetRandomPosition(ref string posArray) {
            int position;

            string randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();

            position = int.Parse(randomChar);

            posArray = posArray.Replace(randomChar, "");

            return position;
        }
    }
}
