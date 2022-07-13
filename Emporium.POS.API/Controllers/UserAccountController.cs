using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.User;
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
    public class UserAccountController: ControllerBase
    {
        private readonly ILogger logger;
        private readonly IUserAccountService userAccountService;
        private readonly IValidateRequestDataService validateRequestDataService;

        public UserAccountController(ILogger logger, IUserAccountService userAccountService, IValidateRequestDataService validateRequestDataService)
        {
            this.logger = logger;
            this.userAccountService = userAccountService;
            this.validateRequestDataService = validateRequestDataService;
        }

        [HttpPost]
        [Route("CreateUserAccount")]
        public DefaultMessageResponse CreateUserAccount([FromBody]UserRequest request)
        {
            try
            {
                var isUserDataValid = validateRequestDataService.ValidateCreateUserAccountRequest(request);
                if (isUserDataValid.HasError)
                    return isUserDataValid;

                logger.LogInformation("Handeling account creation for user: " + request.UserDetails.FirstName + "User Type: " + request.UserDetails.UserType);

                var result = userAccountService.CreateUserAccount(request);

                logger.LogInformation("Returning successful account creation response for user: " + request.UserDetails.FirstName);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to create user account for user: " + request.UserDetails.FirstName);

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("ModifyUserInfo")]
        public DefaultMessageResponse ModifyUserInfo([FromBody]UserDetails request)
        {
            try
            {
                var isUserDataValid = validateRequestDataService.ValidateModifyUserInfoRequest(request);
                if (isUserDataValid.HasError)
                    return isUserDataValid;

                logger.LogInformation("Modifing user account for user: " + request.UserName + "User Type: " + request.UserType);

                var result = userAccountService.ModifyUserInfo(request);

                logger.LogInformation("Returning successful account modify response for user: " + request.UserName);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to modify user account for user: " + request.UserName);

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("ModifyBusinessInfo")]
        public DefaultMessageResponse ModifyBusinessInfo([FromBody]BusinessInfo request)
        {
            try
            {
                var isUserDataValid = validateRequestDataService.ValidateModifyBusinessInfoRequest(request);
                if (isUserDataValid.HasError)
                    return isUserDataValid;

                logger.LogInformation("Handeling business modifing for account: " + request.BusinessName);

                var result = userAccountService.ModifyBusinessInfo(request);

                logger.LogInformation("Returning successful modifing for business account: " + request.BusinessName);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to modify business account: " + request.BusinessName);

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("RemoveUserAccount")]
        public DefaultMessageResponse RemoveUserAccount([FromBody]UserDetails request)
        {
            try
            {
                var isUserDataValid = validateRequestDataService.ValidateRemoveUserAccountRequest(request);
                if (isUserDataValid.HasError)
                    return isUserDataValid;

                logger.LogInformation("Handeling removal of account for user: " + request.UserName);

                var result = userAccountService.RemoveUserAccount(request);

                logger.LogInformation("Returning successful account removal response for user: " + request.UserName);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to remove account for user: " + request.UserName);

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }


    }
}
