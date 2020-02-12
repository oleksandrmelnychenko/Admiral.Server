using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Common.Exceptions.UserExceptions {
    public class UserExceptionCreator<TException> where TException : IUserException, new() {

        private IUserException _userException;

        public string GetUserMessage => _userException.GetUserMessageException;

        public static UserExceptionCreator<TException> Create(string userMessage, object body = null) {
            var instance = new UserExceptionCreator<TException>();

            TException userException = new TException();
            userException.SetUserMessage(userMessage);
            userException.SetBody(body);

            instance._userException = userException;
            return instance;
        }

        public void Throw() {
            throw (Exception)_userException;
        }

        private UserExceptionCreator() {

        }
    }
}
