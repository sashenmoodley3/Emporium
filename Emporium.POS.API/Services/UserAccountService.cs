using Emporium.POS.API.DBContext;
using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ILogger logger;
        private readonly IEncryptionService encryptionService;
        private readonly WebApiDBContext dBContext;
        
        public UserAccountService(ILogger logger, IEncryptionService encryptionService, WebApiDBContext dBContext)
        {
            this.logger = logger;
            this.encryptionService = encryptionService;
            this.dBContext = dBContext;
        }

        public DefaultMessageResponse CreateUserAccount(UserRequest UserRequest)
        {
            var response = new DefaultMessageResponse();

            try
            {
                var uID = Guid.NewGuid().ToString();
                var bID = Guid.NewGuid().ToString();
                UserRequest.UserDetails.Id = uID;

                //TODO
                //UserRequest.UserDetails.UserID = findUserName(UserRequest.UserDetails);
                UserRequest.UserDetails.CreatedTime = DateTime.Now;
                UserRequest.UserDetails.LastLoggedIn = DateTime.Now;

                UserRequest.BusinessInfo.Id = bID;
                UserRequest.BusinessInfo.UserID = uID;
                
                var userSecurity = new UserSecurityInfo()
                {
                    UserID = uID,
                    UserPassword = encryptionService.EncryptValue(UserRequest.UserDetails.Password, UserRequest.UserDetails.UserName),
                    UserName = UserRequest.UserDetails.UserName
                };

                UserRequest.UserDetails.Password = "";
                   
                dBContext.Add(UserRequest.UserDetails);
                dBContext.Add(UserRequest.BusinessInfo);
                dBContext.Add(userSecurity);

                dBContext.SaveChanges();

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to Save new user account: ");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
            
        }

        private string findUserName(UserDetails userDetails)
        {
            throw new NotImplementedException();
        }

        public DefaultMessageResponse ModifyBusinessInfo(BusinessInfo businessInfo)
        {
            var response = new DefaultMessageResponse();

            try
            {
                dBContext.Update(businessInfo);

                dBContext.SaveChanges();

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to modify busniess information");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public DefaultMessageResponse ModifyUserInfo(UserDetails userDetails)
        {
            var response = new DefaultMessageResponse();

            try
            {
                dBContext.Update(userDetails);

                dBContext.SaveChanges();

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to modify user information");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public DefaultMessageResponse RemoveUserAccount(UserDetails userDetails)
        {
            var response = new DefaultMessageResponse();

            try
            {
                var user = dBContext.UserDetails.Find(userDetails.Id);               
                var busniessInfo = dBContext.BusinessInfo.Where<BusinessInfo>(b => b.UserID == user.Id).FirstOrDefault();
                var UserSecurityInfo = dBContext.UserSecurityInfo.Where<UserSecurityInfo>(us => us.UserID == user.Id).FirstOrDefault();

                dBContext.Remove(user);
                dBContext.Remove(busniessInfo);
                dBContext.Remove(UserSecurityInfo);

                dBContext.SaveChanges();

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to remove user account");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
