using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Interfaces
{
    public interface IAuthorizationService
    {
        LoginAuthorizationResponse Login(LoginAuthorizationRequest request);
        DefaultMessageResponse ForgotPassword(ForgotPasswordRequest request);
        DefaultMessageResponse ChangePassword(ChangePasswordRequest request);
        DefaultMessageResponse SetModifyKey(ModifyKey request);
        ModifyKeyResponse ViewModifyKey();
    }
}
