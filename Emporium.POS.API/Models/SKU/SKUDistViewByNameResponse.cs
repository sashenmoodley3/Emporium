using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.SKU
{
    [Serializable]
    public class SKUDistViewByNameResponse
    {
        [JsonProperty(PropertyName = "SKUDetails")]
        public List<SKUDetails> SKUDetails { get; set; }

        [JsonProperty(PropertyName = "HasError")]
        public bool HasError { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "StatusCode")]
        public string StatusCode { get; set; }
    }
}
