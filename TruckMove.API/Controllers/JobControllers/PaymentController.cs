using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.PaymentDto;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Models.VehicleDTOs;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.BLL.Services.PaymentServices;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.Controllers.Primary;
using TruckMove.API.Helper;
using TruckMove.API.Settings;
using Xero.NetStandard.OAuth2.Config;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.Controllers.JobControllers
{
    [ApiController]
    [Route("[controller]")]
#if DEBUG
#else
    [Authorize(Roles = "Administrator,OpsManager,AdminTeam,PayrollTeam")]
#endif
    public class PaymentController : ControllerBase
    {


        private readonly IAuthUserService _authUserService;
        private readonly IPaymentService _PaymentService;



        public PaymentController(IAuthUserService authUserService, IPaymentService PaymentService, IOptions<MySettings> mySettings, IOptions<GoogleMapSettings> googleMapSettings, IJobService jobService, IOptions<XeroConfiguration> xeroConfig)
        {

            _authUserService = authUserService;
            _PaymentService = PaymentService;

        }
        [HttpGet("/Odata/GetQAPendingList")]
        [EnableQuery]

        public async Task<IActionResult> GetQAPendingList()
        {
            var query =  _PaymentService.GetQAPendingList(Convert.ToInt32(_authUserService.GetUserId()));
            return Ok(query);
        }

        [HttpGet("/Odata/GetPayemntList")]
        [EnableQuery]
        public async Task<IActionResult> GetPayemntList(int status)
        {
            var query =  _PaymentService.GetPayemntList(status, Convert.ToInt32(_authUserService.GetUserId()));
            return Ok(query);
        }



        [HttpGet("/GetDetails")]
        public async Task<IActionResult> GetDetails(int jobId, int driverId)
        {
           
            var query = await _PaymentService.GetDetails(jobId, driverId);
            return Ok(query);
        }

        [HttpPost("/ChangePaymentStatus")]
        public async Task<IActionResult> ChangePaymentStatus([FromBody] PaymentStatusDTO status)
        {
            Response response = null;
            if ( (PaymentStatusEnum)status.StatusId == PaymentStatusEnum.Verified || (PaymentStatusEnum)status.StatusId == PaymentStatusEnum.PaymentDone)
            {
                
                response = await _PaymentService.VerifyOrPayPayment(status.JobId??-1,status.DriverId??-1, Convert.ToInt32(_authUserService.GetUserId()), (int)status.StatusId);
               
            }
            else
            {
                 response = await _PaymentService.ChangePaymentStatus(status, Convert.ToInt32(_authUserService.GetUserId()));
                
            }
            
            if (response.Success)
            {

                return Ok(response.data);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }

        }
       

    }
}
