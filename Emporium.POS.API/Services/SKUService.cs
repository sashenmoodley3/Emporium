using Emporium.POS.API.DBContext;
using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.SKU;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Emporium.POS.API.Models.SKU.SKUAPPViewResponse;
using static Emporium.POS.API.Models.SKU.SKUDistViewResponse;

namespace Emporium.POS.API.Services
{
    public class SKUService : ISKUService
    {
        private readonly ILogger logger;
        private readonly WebApiDBContext dBContext;

        public SKUService(ILogger logger, WebApiDBContext dBContext)
        {
            this.logger = logger;
            this.dBContext = dBContext;
        }
        
        public DefaultMessageResponse AddInventory(SKURequest sKURequest)
        {
            var defaultMessageResponse = new DefaultMessageResponse();
            try
            {
                foreach (var sku in sKURequest.SKUDetails)
                {
                    dBContext.SKUDetails.Update(sku);
                }

                dBContext.SaveChanges();

                defaultMessageResponse.HasError = false;
                defaultMessageResponse.Message = "Success";

                return defaultMessageResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to modify SKU");

                defaultMessageResponse.HasError = true;
                defaultMessageResponse.Message = ex.Message;

                return defaultMessageResponse;
            }
        }

        public DefaultMessageResponse AddNewSKU(SKURequest sKURequest)
        {
            var defaultMessageResponse = new DefaultMessageResponse();
            try
            {
                foreach (var sku in sKURequest.SKUDetails)
                {
                    var skuID = Guid.NewGuid().ToString();
                    sku.Id = skuID;

                    dBContext.SKUDetails.Add(sku);
                }

                dBContext.SaveChanges();

                defaultMessageResponse.HasError = false;
                defaultMessageResponse.Message = "Success";

                return defaultMessageResponse;
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message, "Failed to add SKU");

                defaultMessageResponse.HasError = true;
                defaultMessageResponse.Message = ex.Message;

                return defaultMessageResponse;
            }
        }

        public SKUAPPViewResponse AppViewSKU()
        {
            var response = new SKUAPPViewResponse();
            
            try
            {
                var skuList = dBContext.SKUDetails.ToList();

                response.skuAPPViewList = new List<SKUAPPViewList>();

                var uniqueSkuNames = skuList.Select(sk => sk.SKUName).Distinct().ToList();

                foreach (var skuName in uniqueSkuNames)
                {
                    var measurementList = skuList.Where(sk => sk.SKUName == skuName).Select(sm => sm.SKUMeasurementAmount).ToList();
                    var skuImage = skuList.Where(sk => sk.SKUName == skuName).Select(si => si.SKUImage).FirstOrDefault();

                    response.skuAPPViewList.Add(new SKUAPPViewList()
                    {
                        SKUName = skuName,
                        SKUImage = skuImage,
                        SKUMeasurement = measurementList
                    });
                }
                
                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to find SKU's");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public SKUDistViewResponse DistViewSKU()
        {
            var response = new SKUDistViewResponse();

            try
            {
                var skuNames = dBContext.SKUDetails.Select(sk => sk.SKUName).Distinct().ToList();

                response.skuDistViewList = new List<SKUDistViewList>();

                foreach (var skuName in skuNames)
                {
                    var skuImage = dBContext.SKUDetails.Select(si => si.SKUImage).FirstOrDefault();

                    response.skuDistViewList.Add(new SKUDistViewList()
                    {
                        SKUName = skuName,
                        SKUImage = skuImage
                    });
                }

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to find SKU's");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public SKUDistViewByNameResponse DistViewSKUByName(SKUDistViewByNameRequest sKURequest)
        {
            var response = new SKUDistViewByNameResponse();
            try
            {
                var skuDetailsList = dBContext.SKUDetails.Where(sk => sk.SKUName == sKURequest.SKUName).ToList();

                response.SKUDetails = new List<SKUDetails>();
                response.SKUDetails = skuDetailsList;

                response.HasError = false;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to find SKU's");

                response.HasError = true;
                response.Message = ex.Message;

                return response;
            }
        }

        public DefaultMessageResponse ModifySKU(SKURequest sKURequest)
        {
            var defaultMessageResponse = new DefaultMessageResponse();
            try
            {
                foreach (var sku in sKURequest.SKUDetails)
                {
                    dBContext.SKUDetails.Update(sku);
                }

                dBContext.SaveChanges();

                defaultMessageResponse.HasError = false;
                defaultMessageResponse.Message = "Success";

                return defaultMessageResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Failed to modify SKU");

                defaultMessageResponse.HasError = true;
                defaultMessageResponse.Message = ex.Message;

                return defaultMessageResponse;
            }
        }

        public DefaultMessageResponse RemoveSKU(SKURequest sKURequest)
        {
            var defaultMessageResponse = new DefaultMessageResponse();
            try
            {
                foreach (var sku in sKURequest.SKUDetails)
                {
                    dBContext.SKUDetails.Remove(sku);
                }

                dBContext.SaveChanges();

                defaultMessageResponse.HasError = false;
                defaultMessageResponse.Message = "Success";

                return defaultMessageResponse;
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message, "Failed to remove SKU");

                defaultMessageResponse.HasError = true;
                defaultMessageResponse.Message = ex.Message;

                return defaultMessageResponse;
            }
        }
    }
}
