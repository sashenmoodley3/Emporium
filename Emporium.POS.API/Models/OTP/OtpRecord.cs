using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.OTP
{
    [Serializable]
    public class OtpRecord
    {
        [JsonProperty(PropertyName = "Otp")]
        public string Otp { get; set; }

        [JsonProperty(PropertyName = "ClientId")]
        public string ClientID { get; set; }

        [JsonProperty(PropertyName = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty(PropertyName = "ExpirationDate")]
        public DateTime ExpirationDate { get; set; }
    }
}
