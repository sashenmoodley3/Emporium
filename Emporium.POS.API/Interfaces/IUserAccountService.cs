using Emporium.POS.API.Models;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Interfaces
{
    public interface IUserAccountService
    {
        DefaultMessageResponse CreateUserAccount(UserRequest UserRequest);

        DefaultMessageResponse ModifyUserInfo(UserDetails userDetails);

        DefaultMessageResponse ModifyBusinessInfo(BusinessInfo businessInfo);

        DefaultMessageResponse RemoveUserAccount(UserDetails userDetails);
    }
}
