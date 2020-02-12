namespace Admiral.Server.Domain.DataContracts {
    public class SignInErrorResponseModel
    {
        public SignInErrorResponseType SignInErrorResponseType { get; set; }

        public string UserErrorMessage { get; set; }

        public object User { get; set; }

        private SignInErrorResponseModel()
        {
        }

        public static SignInErrorResponseModel New(SignInErrorResponseType signInErrorResponseType, string userErrorMessage, object user = null)
        {
            return new SignInErrorResponseModel
            {
                SignInErrorResponseType = signInErrorResponseType,
                UserErrorMessage = userErrorMessage,
                User = user
            };
        }
    }
}
