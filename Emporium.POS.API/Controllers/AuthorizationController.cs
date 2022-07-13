using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController: ControllerBase
    {
        private readonly ILogger logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IValidateRequestDataService validateRequestDataService;

        public AuthorizationController(ILogger logger, IAuthorizationService authorizationService, IValidateRequestDataService validateRequestDataService)
        {
            this.logger = logger;
            this.authorizationService = authorizationService;
            this.validateRequestDataService = validateRequestDataService;
        }

        [HttpPost]
        [Route("UserLogin")]
        public LoginAuthorizationResponse UserLogin([FromBody]LoginAuthorizationRequest request)
        {
            try
            {
                var isloginDataValid = validateRequestDataService.ValidateLoginRequest(request);
                if (isloginDataValid.HasError)
                    return isloginDataValid;

                logger.LogInformation("Handeling login for user: " + request.UserName);

                var result = authorizationService.Login(request);

                logger.LogInformation("Returning successful login for user: " + request.UserName);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to login with userID: " + request.UserName);

                Response.StatusCode = 500;

                return new LoginAuthorizationResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("ChangeUserPassword")]
        public DefaultMessageResponse ChangeUserPassword([FromBody]ChangePasswordRequest request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateChangePasswordRequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Handeling change password for user: " + request.UserID);

                var result = authorizationService.ChangePassword(request);

                logger.LogInformation("Returning successful change password response for user: " + request.UserID);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to change password for user: " + request.UserID);

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("ForgotUserPassword")]
        public DefaultMessageResponse ForgotUserPassword([FromBody]ForgotPasswordRequest request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateForgotPasswordRequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Handeling forgot password for user: " + request.UserID);

                var result = authorizationService.ForgotPassword(request);

                logger.LogInformation("Returning successful forgot password response for user: " + request.UserID);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send forgotten password to user: " + request.UserID);

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("SetModifyKey")]
        public DefaultMessageResponse SetModifyKey([FromBody]ModifyKey request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateModifyKeyRequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Setting new Modify Key");

                var result = authorizationService.SetModifyKey(request);

                logger.LogInformation("Returning successful modify response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to set modify key");

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("ViewModifyKey")]
        public ModifyKeyResponse ViewModifyKey()
        {
            try
            {
                logger.LogInformation("Getting modify key");

                var result = authorizationService.ViewModifyKey();

                logger.LogInformation("Returning successful modify response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to set modify key");

                Response.StatusCode = 500;

                return new ModifyKeyResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }
    }
}
