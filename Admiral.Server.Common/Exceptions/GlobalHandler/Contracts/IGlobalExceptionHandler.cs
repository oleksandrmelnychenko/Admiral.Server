using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Admiral.Server.Common.Exceptions.GlobalHandler.Contracts {
    public interface IGlobalExceptionHandler {
        Task HandleException(HttpContext httpContext, IExceptionHandlerFeature exceptionHandlerFeature, bool isDevelopmentMode);
    }
}
