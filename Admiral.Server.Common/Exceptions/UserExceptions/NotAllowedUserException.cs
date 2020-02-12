using System;

namespace Admiral.Server.Common.Exceptions.UserExceptions {
    public class NotAllowedUserException : Exception, IUserException {
        public string GetUserMessageException { get; private set; }
        public object Body { get; private set; }

        public void SetUserMessage(string message) {
            GetUserMessageException = message;
        }

        public void SetBody(object body) {
            Body = body;
        }
    }
}
