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
        private readonly MySettings _mySettings;
        private readonly IJobService _jobService;



        public PaymentController(IAuthUserService authUserService, IPaymentService PaymentService, IOptions<MySettings> mySettings, IOptions<GoogleMapSettings> googleMapSettings, IJobService jobService)
        {

            _authUserService = authUserService;
            _PaymentService = PaymentService;
            _mySettings = mySettings.Value;
            _jobService = jobService;

        }
        [HttpGet("/Odata/GetQAPendingList")]
        [EnableQuery]

        public async Task<IActionResult> GetQAPendingList()
        {
            var query = _PaymentService.GetQAPendingList();
            return Ok(query);
        }

        [HttpGet("/Odata/GetPayemntQADoneList")]
        [EnableQuery]
        public async Task<IActionResult> GetPayemntQADoneList()
        {
            var query = _PaymentService.GetPayemntQADoneList();
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
            

            var response = await _PaymentService.ChangePaymentStatus(status, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok();
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
    }
}
