using Emporium.POS.API.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.Authorization
{
    [Serializable]
    public class LoginAuthorizationResponse
    {
        [JsonProperty(PropertyName = "HasError")]
        public bool HasError { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "StatusCode")]
        public string StatusCode { get; set; }

        [JsonProperty(PropertyName = "SecurityToken")]
        public string SecurityToken { get; set; }

        [JsonProperty(PropertyName = "UserRequest")]
        public UserRequest UserRequest { get; set; }
    }
}
