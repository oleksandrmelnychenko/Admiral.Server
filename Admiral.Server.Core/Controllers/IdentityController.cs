using Admiral.Server.Common.Exceptions.UserExceptions;
using Admiral.Server.Common.ResponseBuilder.Contracts;
using Admiral.Server.Common.WebApi;
using Admiral.Server.Common.WebApi.RoutingConfiguration;
using Admiral.Server.Domain.DataContracts;
using Admiral.Server.Domain.Entities.Identity;
using Admiral.Server.Services.IdentityServices.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Admiral.Server.Core.Controllers {
    [AssignControllerRoute(WebApiEnvironmnet.Current, WebApiVersion.ApiVersion1, ApplicationSegments.UserIdentity)]
    public class IdentityController : WebApiControllerBase {
        private readonly IUserIdentityService _userIdentityService;

        public IdentityController(IUserIdentityService userIdentityService, IResponseFactory responseFactory, IStringLocalizer localizer)
            : base(responseFactory, localizer) {
            _userIdentityService = userIdentityService;
        }

        [Authorize]
        [HttpGet]
        [AssignActionRoute(IdentitySegments.VALIDATE_TOKEN)]
        public async Task<IActionResult> ValidateToken() {
            try {
                UserAccount user = await _userIdentityService.ValidateToken(User);
                return Ok(SuccessResponseBody(user, Localizer["Token validated successfully"]));
            } catch (InvalidIdentityException exc) {
                return Unauthorized(ErrorResponseBody(exc.GetUserMessageException, HttpStatusCode.Unauthorized, exc.Body));
            } catch (Exception exc) {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [AssignActionRoute(IdentitySegments.NEW_ACCOUNT)]
        public async Task<IActionResult> NewUser([FromBody] NewUserDataContract newUserDataContract) {
            try {
                if (newUserDataContract == null) throw new ArgumentNullException("NewUserDataContract");

                UserAccount user = await _userIdentityService.NewUser(newUserDataContract);

                return Ok(SuccessResponseBody(user, Localizer["New user has been created successfully"]));
            } catch (InvalidIdentityException exc) {
                return BadRequest(ErrorResponseBody(exc.GetUserMessageException, HttpStatusCode.BadRequest, exc.Body));
            } catch (Exception exc) {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [AssignActionRoute(IdentitySegments.SIGN_IN)]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDataContract authenticateDataContract) {
            try {
                if (authenticateDataContract == null) throw new ArgumentNullException("AuthenticationDataContract");

                UserAccount user = await _userIdentityService.SignInAsync(authenticateDataContract);

                return Ok(SuccessResponseBody(user, Localizer["User logged in successfully"]));
            } catch (InvalidIdentityException exc) {
                return BadRequest(ErrorResponseBody(exc.GetUserMessageException, HttpStatusCode.BadRequest, exc.Body));
            } catch (Exception exc) {
                Log.Error(exc.Message);
                return BadRequest(ErrorResponseBody(exc.Message, HttpStatusCode.BadRequest));
            }
        }

    }
}
