using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Models;
using Emporium.POS.API.Models.SKU;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SKUController: ControllerBase
    {
        private readonly ILogger logger;
        private readonly ISKUService SKUService;
        private readonly IValidateRequestDataService validateRequestDataService;

        public SKUController(ILogger logger, ISKUService sKUService, IValidateRequestDataService validateRequestDataService)
        {
            this.logger = logger;
            SKUService = sKUService;
            this.validateRequestDataService = validateRequestDataService;
        }

        [HttpPost]
        [Route("AddNewSKU")]
        public DefaultMessageResponse AddNewSKU([FromBody]SKURequest request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateSKURequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Adding New SKU");

                var result = SKUService.AddNewSKU(request);

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Add new SKU");

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }
        
        [HttpPost]
        [Route("ModifySKU")]
        public DefaultMessageResponse ModifySKU([FromBody]SKURequest request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateSKURequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Modifying " + request.SKUDetails.Count +" SKU(s)");

                var result = SKUService.ModifySKU(request);

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Modify SKU");

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("RemoveSKU")]
        public DefaultMessageResponse RemoveSKU([FromBody]SKURequest request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateSKURequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Adding New SKU");

                var result = SKUService.RemoveSKU(request);

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Add new SKU");

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("AddInventory")]
        public DefaultMessageResponse AddInventory([FromBody]SKURequest request)
        {
            try
            {
                var isDataValid = validateRequestDataService.ValidateSKURequest(request);
                if (isDataValid.HasError)
                    return isDataValid;

                logger.LogInformation("Adding stock for SKU");

                var result = SKUService.AddInventory(request);

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Add new SKU");

                Response.StatusCode = 500;

                return new DefaultMessageResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("ViewSKUAppDetails")]
        public SKUAPPViewResponse ViewSKUAppDetails()
        {
            try
            {
                logger.LogInformation("Getting SKU's for app view");

                var result = SKUService.AppViewSKU();

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Add new SKU");

                Response.StatusCode = 500;

                return new SKUAPPViewResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("ViewSKUDistDetails")]
        public SKUDistViewResponse ViewSKUDistDetails()
        {
            try
            {
                logger.LogInformation("Getting SKU's for distribution view");

                var result = SKUService.DistViewSKU();

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Add new SKU");

                Response.StatusCode = 500;

                return new SKUDistViewResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("ViewSKUDistDetailsByName")]
        public SKUDistViewByNameResponse ViewSKUDistDetailsByName([FromBody]SKUDistViewByNameRequest sKUDistViewByNameRequest)
        {
            try
            {
                logger.LogInformation("Getting SKU's by name for distribution view");

                var result = SKUService.DistViewSKUByName(sKUDistViewByNameRequest);

                logger.LogInformation("Returning successful SKU response");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to Add new SKU");

                Response.StatusCode = 500;

                return new SKUDistViewByNameResponse()
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }
    }
}
