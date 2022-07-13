using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.SKU
{
    [Serializable]
    public class SKUAPPViewResponse
    {
        [JsonProperty(PropertyName = "skuAPPViewList")]
        public List<SKUAPPViewList> skuAPPViewList { get; set; }
    
        [JsonProperty(PropertyName = "HasError")]
        public bool HasError { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "StatusCode")]
        public string StatusCode { get; set; }

        public class SKUAPPViewList
        {
            [JsonProperty(PropertyName = "SKUName")]
            public string SKUName { get; set; }

            [JsonProperty(PropertyName = "SKUMeasurement")]
            public List<string> SKUMeasurement { get; set; }

            [JsonProperty(PropertyName = "SKUImage")]
            public string SKUImage { get; set; }
        }
    }
}
