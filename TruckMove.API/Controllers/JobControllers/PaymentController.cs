using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
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
       

        public PaymentController(IAuthUserService authUserService, IPaymentService PaymentService, IOptions<MySettings> mySettings, IOptions<GoogleMapSettings> googleMapSettings)
        {

            _authUserService = authUserService;
            _PaymentService = PaymentService;
            _mySettings = mySettings.Value;
           

        }
        [HttpGet("/Odata/PayemntQAList")]
        [EnableQuery]

        public async Task<IActionResult> PayemntQAList()
        {
            var query = _PaymentService.GetAllUnpaidPaymentsForDrivers();
            return Ok(query);
        }
        [HttpGet("/GetCalculatedLegPaymentsAsync")]
        public async Task<IActionResult> GetCalculatedLegPaymentsAsync(int jobId, int driverId)
        {
            var query = await _PaymentService.GetCalculatedLegPaymentsAsync(jobId, driverId);
            return Ok(query);
        }

    }
}
