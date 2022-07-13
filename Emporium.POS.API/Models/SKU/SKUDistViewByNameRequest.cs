using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.SKU
{
    [Serializable]
    public class SKUDistViewByNameRequest
    {
        [JsonProperty(PropertyName = "SKUName")]
        public String SKUName { get; set; }
    }
}
