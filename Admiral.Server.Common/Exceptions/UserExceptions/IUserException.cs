using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Common.Exceptions.UserExceptions {
    public interface IUserException {
        string GetUserMessageException { get; }

        object Body { get; }

        void SetUserMessage(string message);

        void SetBody(object body);
    }
}
