using Admiral.Server.Common.ResponseBuilder.Contracts;

namespace Admiral.Server.Common.ResponseBuilder {
    public class ResponseFactory : IResponseFactory {
        public IWebResponse GetSuccessReponse() {
            return new SuccessResponse();
        }

        public IWebResponse GetErrorResponse() {
            return new ErrorResponse();
        }
    }
}
