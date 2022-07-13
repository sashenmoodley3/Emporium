using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.OTP
{
    [Serializable]
    public class OtpVerifyRequest
    {
        [JsonProperty(PropertyName = "ClientId")]
        public string ClientID { get; set; }

        [JsonProperty(PropertyName = "Otp")]
        public string Otp { get; set; }
    }
}
