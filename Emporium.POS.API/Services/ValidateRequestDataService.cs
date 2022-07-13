using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Authorization;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.SKU;
using Emporium.POS.API.Models.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Services
{
    public class ValidateRequestDataService : IValidateRequestDataService
    {
        private readonly ILogger logger;

        public ValidateRequestDataService(ILogger logger)
        {
            this.logger = logger;
        }

        public DefaultMessageResponse ValidateCreateUserAccountRequest(UserRequest request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if(request.UserDetails == null || request.BusinessInfo == null)
            {
                response.Message = "User/Business information provided was empty";
                return response;
            }

            if (request.UserDetails.FirstName == null || request.BusinessInfo.BusinessName == null)
            {
                response.Message = "User/Business name information was empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public DefaultMessageResponse ValidateModifyUserInfoRequest(UserDetails request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "User information provided was empty";
                return response;
            }

            if (request.Id == null)
            {
                response.Message = "User ID was empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public DefaultMessageResponse ValidateModifyBusinessInfoRequest(BusinessInfo request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "Business information provided was empty";
                return response;
            }

            if (request.Id == null)
            {
                response.Message = "Business ID was empty";
                return response;
            }

            response.HasError = false;
            return response;
        }
        
        public DefaultMessageResponse ValidateRemoveUserAccountRequest(UserDetails request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "User information provided was empty";
                return response;
            }

            if (request.Id == null)
            {
                response.Message = "User ID was empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public LoginAuthorizationResponse ValidateLoginRequest(LoginAuthorizationRequest request)
        {
            var response = new LoginAuthorizationResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "Login information provided was empty";
                return response;
            }

            if (request.UserName == null || request.MobilNumber == null || request.Password == null)
            {
                response.Message = "Values in login request where empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public DefaultMessageResponse ValidateChangePasswordRequest(ChangePasswordRequest request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "Change password information provided was empty";
                return response;
            }

            if (request.UserID == null || request.Password == null)
            {
                response.Message = "Values in change password request where empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public DefaultMessageResponse ValidateForgotPasswordRequest(ForgotPasswordRequest request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "Forgot password information provided was empty";
                return response;
            }

            if (request.UserID == null || request.MobileNumber == null)
            {
                response.Message = "Values in forgot password request where empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public DefaultMessageResponse ValidateSKURequest(SKURequest request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null || request.SKUDetails.Count == 0)
            {
                response.Message = "SKU information provided was empty";
                return response;
            }

            if (request.SKUDetails[0].Id == null)
            {
                response.Message = "Values in SKU request where empty";
                return response;
            }

            response.HasError = false;
            return response;
        }

        public DefaultMessageResponse ValidateModifyKeyRequest(ModifyKey request)
        {
            var response = new DefaultMessageResponse();
            response.HasError = true;

            if (request == null)
            {
                response.Message = "Modify Key provided was empty";
                return response;
            }

            if (request.Key == null)
            {
                response.Message = "Key modify Key is empty";
                return response;
            }

            response.HasError = false;
            return response;
        }
    }
}
