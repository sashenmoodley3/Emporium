using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.SKU
{
    [Serializable]
    public class SKURequest
    {
       [JsonProperty(PropertyName = "SKUDetails")]
       public List<SKUDetails> SKUDetails { get; set; }

        [JsonProperty(PropertyName = "ModifyKey")]
        public string ModifyKey { get; set; }
    }
}
