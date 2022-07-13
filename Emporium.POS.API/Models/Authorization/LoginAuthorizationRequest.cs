using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.Authorization
{
    [Serializable]
    public class LoginAuthorizationRequest
    {
        
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "MobilNumber")]
        public string MobilNumber { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
    }
}
