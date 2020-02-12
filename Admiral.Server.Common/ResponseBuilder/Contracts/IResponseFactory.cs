namespace Admiral.Server.Common.ResponseBuilder.Contracts {
    public interface IResponseFactory {
        IWebResponse GetSuccessReponse();
        IWebResponse GetErrorResponse();
    }
}
