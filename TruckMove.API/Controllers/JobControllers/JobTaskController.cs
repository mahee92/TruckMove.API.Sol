using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.TaskDTOs;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.DAL.Models;
using TruckMove.API.Helper;
using TruckMove.API.Settings;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.Controllers.JobControllers
{
    [ApiController]
    [Route("[controller]")]
#if DEBUG
#else
    [Authorize(Roles = "Administrator,OpsManager,AdminTeam,PayrollTeam")]
#endif
    public class JobTaskController : ControllerBase
    {

        private readonly IAuthUserService _authUserService;
        private readonly IJobTaskService _jobTaskService;
        private readonly MySettings _mySettings;

        public JobTaskController(IAuthUserService authUserService, IJobTaskService jobtaskService, IOptions<MySettings> mySettings)
        {

            _authUserService = authUserService;
            _jobTaskService = jobtaskService;
            _mySettings = mySettings.Value;

        }
        #region Permits and Plate
        [HttpPost("PermitsAndPlate/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] PermitsAndPlateDto permitsAndPlate)
        {
            Response<PermitsAndPlateDto> response = await _jobTaskService.PermitsAndPlatePostPut(permitsAndPlate, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("PermitsAndPlate/Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response response = await _jobTaskService.PermitsAndPlateDeleteAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        #endregion

        #region shared
        [HttpPost("Attachment/Post")]
        public async Task<IActionResult> PostAsync([FromBody] AttachmentDto attachment)
        {
            Response<AttachmentDto> response = await _jobTaskService.AttachmentPostAsync(attachment, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("Attachment/Delete")]
        public async Task<IActionResult> VehicleImageDeleteAsync(int id)
        {
            Response response = await _jobTaskService.ImageDeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        #endregion

        #region Accommodation
        [HttpPost("Accommodation/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] AccommodationDto accommodation)
        {
            Response<AccommodationDto> response = await _jobTaskService.AccommodationPostPut(accommodation, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete("Accommodation/Delete")]
        public async Task<IActionResult> AccommodationDeleteAsync(int id)
        {
            Response response = await _jobTaskService.AccommodationDeleteAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        #endregion

        #region PublicTransport
        [HttpPost("PublicTransport/PostPut")]
        public async Task<IActionResult> PublicTransportPostPutAsync([FromBody] PublicTransportDto transport)
        {
            Response<PublicTransportOutputDto> response = await _jobTaskService.PublicTransportPostPut(transport, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete("PublicTransport/Delete")]
        public async Task<IActionResult> PublicTransportDeleteAsync(int id)
        {
            Response response = await _jobTaskService.PublicTransportDeleteAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        #endregion

        #region Purchase
        [HttpPost("Purchase/PostPut")]
        public async Task<IActionResult> PurchasePostPutAsync([FromBody] PurchaseDto purchase)
        {
            Response<PurchaseOutputDto> response = await _jobTaskService.PurchasePostPut(purchase, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete("Purchase/Delete")]
        public async Task<IActionResult> PurchaseDeleteAsync(int id)
        {
            Response response = await _jobTaskService.PurchaseDeleteAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        #endregion

        #region Delays

        [HttpPost("Delays/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] DelayDto delay)
        {
            Response<DelayDto> response = await _jobTaskService.DelayPostPut(delay, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete("Delays/Delete")]
        public async Task<IActionResult> DelaysDeleteAsync(int id)
        {
            Response response = await _jobTaskService.DelayDeleteAsync(id);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        #endregion

        #region MyTask
        [HttpGet("MyTask")]
        public async Task<IActionResult> GetMyTasks()
        {
            Response<MyTaskDto> response = await _jobTaskService.GetMyTasks(Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpGet("GetGraphData")]
        public async Task<IActionResult> GetGraphData()
        {
            Response<GraphData> response = await _jobTaskService.GetGraphData();
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        #endregion

    }
}
