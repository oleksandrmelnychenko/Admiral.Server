using System.Text.RegularExpressions;

namespace Admiral.Server.Common.Helpers {
    public static class Validator {
        public static bool IsEmailValid(string email) =>
            Regex.IsMatch(
                email,
                @"^(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$"
            );
    }
}
