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
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.Controllers.Primary;
using TruckMove.API.Helper;
using TruckMove.API.Settings;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TruckMove.API.Controllers.JobControllers
{
    [ApiController]
    [Route("[controller]")]
#if DEBUG
#else
    [Authorize(Roles = "Administrator,OpsManager")]
#endif
    public class JobController : ControllerBase
    {

        private readonly IAuthUserService _authUserService;
        private readonly IJobService _jobService;
        private readonly MySettings _mySettings;

        public JobController(IAuthUserService authUserService, IJobService jobService, IOptions<MySettings> mySettings)
        {
           
            _authUserService = authUserService;
            _jobService = jobService;
            _mySettings = mySettings.Value;

        }

        #region Job
        [HttpGet("GetNextJobId")]
        public async Task<IActionResult> GetNextJobId()
        {
            var response = await _jobService.GetNextJobId();
            if (response.Success)
            {
                return Ok(response.data);
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpPost("PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] JobDto job)
        {
            Response<JobDto> response = await _jobService.PostPutAsync(job, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            Response<JobOutPutDTO> response = await _jobService.GetAsync(id);

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

        #region Contacts
        [HttpPost("{id}/Contacts/AddDelete")]
        public async Task<IActionResult> AddDelete(int id, [FromBody] List<int> contacts)
        {
            var response = await _jobService.ContactAddDelete(id, contacts);
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

        #region Vehicle
        [HttpPost("Vehicle/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] VehicleDto vehicle)
        {
            Response<VehicleDto> response = await _jobService.VehiclePostPutAsync(vehicle, Convert.ToInt32(_authUserService.GetUserId()));
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

        #region WayPoint
        [HttpPost("WayPoint/AddDelete")]
        public async Task<IActionResult> AddDeleteWayPoint([FromBody] List<WayPointDto> wayPoints)
        {
            var response = await _jobService.WayPointAddDelete(wayPoints);
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


        #region Shared

        [HttpPost("Note/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] NoteDto note)
        {
            Response<NoteDto> response = await _jobService.NotePostPutAsync(note, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("Note/Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response response = await _jobService.NoteDeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpPost("Image/Post")]
        public async Task<IActionResult> PostAsync([FromBody] ImageDto image)
        {
            Response<ImageDto> response = await _jobService.ImagePostAsync(image, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("Image/Delete")]
        public async Task<IActionResult> VehicleImageDeleteAsync(int id)
        {
            Response response = await _jobService.ImageDeleteAsync(id);
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

        #region Trailer
        [HttpPost("Trailer/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] TrailerDto trailer)
        {
            Response<TrailerDto> response = await _jobService.TrailerPostPutAsync(trailer, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete]
        [HttpPost("Trailer/Delete")]
        public async Task<IActionResult> TrailerDeleteAsync(int id)
        {
            Response response = await _jobService.TrailerDeleteAsync(id);
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



    }
}
