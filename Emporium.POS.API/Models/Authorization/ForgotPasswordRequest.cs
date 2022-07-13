using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.Authorization
{
    [Serializable]
    public class ForgotPasswordRequest
    {
        [JsonProperty(PropertyName = "UserID")]
        public string UserID { get; set; }
        [JsonProperty(PropertyName = "MobileNumber")]
        public string MobileNumber { get; set; }
    }
}
