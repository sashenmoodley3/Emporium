using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.Business
{
    [Serializable]
    public class BusinessInfo
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserID")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "BusinessName")]
        public string BusinessName { get; set; }

        [JsonProperty(PropertyName = "BusinessType")]
        public string BusinessType { get; set; }

        [JsonProperty(PropertyName = "BusinessLocation")]
        public string BusinessLocation { get; set; }

        [JsonProperty(PropertyName = "BusinessImage")]
        public string BusinessImage { get; set; }
    }
}
