using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Common.Exceptions.UserExceptions {
    public class InvalidIdentityException : Exception, IUserException {
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
