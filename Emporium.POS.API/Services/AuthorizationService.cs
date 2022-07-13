using Emporium.POS.API.DBContext;
using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Authorization;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ILogger logger;
        private readonly ITokenService tokenService;
        private readonly IEncryptionService encryptionService;
        private readonly WebApiDBContext dBContext;

        public AuthorizationService(ILogger logger, ITokenService tokenService, IEncryptionService encryptionService, WebApiDBContext dBContext)
        {
            this.logger = logger;
            this.tokenService = tokenService;
            this.encryptionService = encryptionService;
            this.dBContext = dBContext;
        }

        public LoginAuthorizationResponse Login(LoginAuthorizationRequest request)
        {
            var response = new LoginAuthorizationResponse();

            try
            {
                var userSecInfo = dBContext.UserSecurityInfo.Where<UserSecurityInfo>(us => us.UserName == request.UserName).FirstOrDefault();

                if (userSecInfo == null) {
                    response.HasError = true;
                    response.Message = "User Name or Password is incorrect";

                    return response;
                }

                var matchingPWD = encryptionService.EncryptValue(request.Password, request.UserName);

                if (!matchingPWD.Equals(userSecInfo.UserPassword))
                {
                    response.HasError = true;
                    response.Message = "User Name or Password is incorrect";

                    return response;
                }

                response.SecurityToken = tokenService.GetToken();
                response.HasError = false;
                response.Message = "Success";

                var userInfo = dBContext.UserDetails.Find(userSecInfo.UserID);
                var busniessInfo = dBContext.BusinessInfo.Where<BusinessInfo>(b => b.UserID == userInfo.Id).FirstOrDefault();

                response.UserRequest = new UserRequest();
                
                response.UserRequest.UserDetails = new UserDetails();
                response.UserRequest.BusinessInfo = new BusinessInfo();

                response.UserRequest.UserDetails = userInfo;
                response.UserRequest.BusinessInfo = busniessInfo;

                return response;
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public DefaultMessageResponse ChangePassword(ChangePasswordRequest request)
        {
            var response = new DefaultMessageResponse();

            try
            {
                var userSecInfo = dBContext.UserSecurityInfo.Where<UserSecurityInfo>(us => us.UserID == request.UserID).FirstOrDefault();

                if (userSecInfo == null)
                {
                    response.HasError = true;
                    response.Message = "Missing user security info";

                    return response;
                }

                var newPassword = encryptionService.EncryptValue(request.Password, request.UserName);

                userSecInfo.UserPassword = newPassword;

                dBContext.UserSecurityInfo.Update(userSecInfo);
                dBContext.SaveChanges();

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public DefaultMessageResponse ForgotPassword(ForgotPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public DefaultMessageResponse SetModifyKey(ModifyKey request)
        {
            var response = new DefaultMessageResponse();

            try
            {
                dBContext.ModifyKey.Update(request);

                dBContext.SaveChanges();

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to find SKU's");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public ModifyKeyResponse ViewModifyKey()
        {
            var response = new ModifyKeyResponse();

            try
            {
                var modifyKey = dBContext.ModifyKey.FirstOrDefault().Key;

                response.Key = modifyKey;
                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to find SKU's");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
