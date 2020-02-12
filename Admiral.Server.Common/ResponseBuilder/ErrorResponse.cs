using Admiral.Server.Common.ResponseBuilder.Contracts;
using System.Net;

namespace Admiral.Server.Common.ResponseBuilder {
    public class ErrorResponse : IWebResponse {
        public object Body { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
