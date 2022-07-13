using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Models.SKU
{
    [Serializable]
    public class SKUDetails
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "SKUName")]
        public string SKUName { get; set; }

        [JsonProperty(PropertyName = "SKUPrice")]
        public Decimal SKUPrice { get; set; }

        [JsonProperty(PropertyName = "InventoryAmount")]
        public int InventoryAmount { get; set; }

        [JsonProperty(PropertyName = "SKUImage")]
        public string SKUImage { get; set; }

        [JsonProperty(PropertyName = "SKUMeasurementAmount")]
        public string SKUMeasurementAmount { get; set; }

        [JsonProperty(PropertyName = "KnownSKU")]
        public string KnownSKU { get; set; }
    }
}
