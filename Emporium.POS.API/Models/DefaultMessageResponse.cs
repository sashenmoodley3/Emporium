using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models
{
    [Serializable]
    public class DefaultMessageResponse
    {
        [JsonProperty(PropertyName = "HasError")]
        public bool HasError { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "StatusCode")]
        public string StatusCode { get; set; }
    }
}
