using Emporium.POS.API.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.User
{
    public class UserRequest
    {
        public UserDetails UserDetails { get; set; }
        public BusinessInfo BusinessInfo { get; set; }
    }
}
