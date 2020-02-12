using Admiral.Server.Common.ResponseBuilder.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Admiral.Server.Common.WebApi {
    public abstract class WebApiControllerBase : Controller {
        private readonly IResponseFactory _responseFactory;

        protected IStringLocalizer Localizer { get; private set; }

        /// <summary>
        ///     ctor().
        /// </summary>
        protected WebApiControllerBase(IResponseFactory responseFactory, IStringLocalizer localizer) {
            _responseFactory = responseFactory;
            Localizer = localizer;
        }

        protected IWebResponse SuccessResponseBody(object body, string message = "") {
            IWebResponse response = _responseFactory.GetSuccessReponse();
            response.Body = body;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = message;

            return response;
        }

        protected IWebResponse ErrorResponseBody(string message, HttpStatusCode statusCode, object body = null) {
            IWebResponse response = _responseFactory.GetErrorResponse();

            response.Message = message;
            response.StatusCode = statusCode;
            response.Body = body;

            return response;
        }
    }
}
