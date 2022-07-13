using Emporium.POS.API.Models;
using Emporium.POS.API.Models.SKU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Interfaces
{
    public interface ISKUService
    {
        DefaultMessageResponse AddNewSKU(SKURequest sKURequest);

        DefaultMessageResponse ModifySKU(SKURequest sKURequest);

        DefaultMessageResponse RemoveSKU(SKURequest sKURequest);

        SKUAPPViewResponse AppViewSKU();

        SKUDistViewResponse DistViewSKU();

        SKUDistViewByNameResponse DistViewSKUByName(SKUDistViewByNameRequest sKURequest);

        DefaultMessageResponse AddInventory(SKURequest sKURequest);
    }
}
