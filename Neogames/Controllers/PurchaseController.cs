using BL;
using CoreProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Neogames.Controllers
{
    public class PurchaseController : ApiController
    {

        private MainDataFetcher _mainDataFetcher = new MainDataFetcher();

        /// <summary>
        /// Get the purchase data from start date in the buld size
        /// returning request id GUID for sequential request
        /// 
        ///  Demo url: http://localhost:[Port]/api/purchase/getfromdate?startDate=20191126T140931Z&bulkSize=5
        ///  moving +2 hours forward due to time zone
        /// </summary>
        /// <param name="startDateString"></param>
        /// <param name="bulkSize"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/purchase/getfromdate")]
        public IHttpActionResult StartFromDate([FromUri]string startDate, [FromUri]int bulkSize)
        {
            try
            {
                // need to validate parameters before continue ...
                if (bulkSize <= 0)
                    return BadRequest("bulk size must be greater euqal than 1");

                var startDateDT = Utilities.BuildDateTimeFromYAFormat(startDate);

                List<PurchaseInfo> result = _mainDataFetcher.GetBulk(startDateDT, bulkSize, out string requestId);

                if (result.Count == 0)
                    // could also consider:
                    // return Content(HttpStatusCode.NoContent, "no items found");
                    return Ok(new { message =  "no items found" });

                return Ok(new { data = result, requestId, message = "use the request id in the sequential requests" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex); // +also write to log file

                // consider which error to send to customer
                // choose between bad-request and internal-error
                return BadRequest("Something went wrong ... check the parameter you send. please contact support");
            }

        }

        /// <summary>
        /// Continue to get the next bulk from last position
        /// depending on the request id GUID
        /// 
        /// Since the API should continue, there is no sense in using a date time here
        /// if the user wants a different date then he should use the GetBulkFromGivenDateTime
        /// otherwise it makes more sense to continue from the last point
        /// assuming multiple customers will be using this app- so they will be using request id
        /// 
        ///  Demo url: http://localhost:[Port]/api/purchase/continue?bulkSize=5&requestid=[guide from getfromdate]
        /// </summary>
        /// <param name="bulkSize"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/purchase/continue")]
        public IHttpActionResult Continue([FromUri]int bulkSize, [FromUri]string requestid)
        {
            try
            {
                // need to validate parameters before continue ...
                if (bulkSize <= 0)
                    return BadRequest("bulk size must be greater euqal than 1");

                if (!_mainDataFetcher.CheckIfRequestIdExist(requestid))
                    return BadRequest($"Request id {requestid} does not exist");

                List<PurchaseInfo> result = _mainDataFetcher.ContinueToNextBulk(requestid, bulkSize);

                if (result.Count == 0)
                    // could also consider:
                    //return Content(HttpStatusCode.NoContent, "no more items");
                    return Ok(new { message = "no more items" });

                return Ok(new { result });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex); // +also write to log file

                // consider which error to send to customer
                // choose between bad-request and internal-error
                return BadRequest("Something went wrong ... check the parameter you send. please contact support");
            }

        }
    }
}
