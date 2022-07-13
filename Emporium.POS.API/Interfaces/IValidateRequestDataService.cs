using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Authorization;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.SKU;
using Emporium.POS.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Interfaces
{
    public interface IValidateRequestDataService
    {
        DefaultMessageResponse ValidateCreateUserAccountRequest(UserRequest request);

        DefaultMessageResponse ValidateModifyUserInfoRequest(UserDetails request);

        DefaultMessageResponse ValidateModifyBusinessInfoRequest(BusinessInfo request);

        DefaultMessageResponse ValidateRemoveUserAccountRequest(UserDetails request);

        LoginAuthorizationResponse ValidateLoginRequest(LoginAuthorizationRequest request);

        DefaultMessageResponse ValidateChangePasswordRequest(ChangePasswordRequest request);

        DefaultMessageResponse ValidateForgotPasswordRequest(ForgotPasswordRequest request);

        DefaultMessageResponse ValidateModifyKeyRequest(ModifyKey request);


        //-------------------------SKU--------------------------------------------------------

        DefaultMessageResponse ValidateSKURequest(SKURequest request);

    }
}
