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
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.Controllers.JobControllers
{
    [ApiController]
    [Route("[controller]")]
#if DEBUG
#else
    [Authorize(Roles = "Administrator,OpsManager,AdminTeam")]
#endif
    public class JobController : ControllerBase
    {


        private readonly IAuthUserService _authUserService;
        private readonly IJobService _jobService;
        private readonly MySettings _mySettings;
        private readonly GoogleMapSettings _googleMapSettings;

        public JobController(IAuthUserService authUserService, IJobService jobService, IOptions<MySettings> mySettings, IOptions<GoogleMapSettings> googleMapSettings)
        {
           
            _authUserService = authUserService;
            _jobService = jobService;
            _mySettings = mySettings.Value;
            _googleMapSettings = googleMapSettings.Value;

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
            Response<JobDto> response = await _jobService.PostPutAsync(job, Convert.ToInt32(_authUserService.GetUserId()), _googleMapSettings.ApiKey);
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

        [HttpGet("/Odata/Job/GetAll")]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            //string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiZHJpdmVyQGV4YW1wbGUuY29tIiwibmFtZWlkIjoiMjEiLCJyb2xlIjoiRHJpdmVyIiwibmJmIjoxNzE5MzM0NTIyLCJleHAiOjE3MTkzMzgxMjIsImlhdCI6MTcxOTMzNDUyMiwiaXNzIjoiaHR0cHM6Ly92dG10cnVja21vdmUuYXBpLmRldi5yaXZlcmluYS5kaWdpdGFsLyIsImF1ZCI6Imh0dHBzOi8vdnRtdHJ1Y2ttb3ZlLmFwaS5kZXYucml2ZXJpbmEuZGlnaXRhbC8ifQ.qMI46lgenS0kKwDsYf8HIew_R-IzgSIrT713Dl1m60";
            //await JobApiClient.SetJwtToken();
            //string result = await JobApiClient.ApiCallAsync();

            var query = _jobService.GetAllAsync();
            //var count = query.Count();
            return Ok(query);
        }

        [HttpGet("IsDriverChangeAllowed")]
        public async Task<IActionResult> IsDriverChangeAllowed(int jobId)
        {
            Response response = await _jobService.IsDriverChangeAllowed(jobId);
            if (response.Success)
            {

                return Ok();
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }



        //[HttpPost("UpdateStatus")]
        //public async Task<IActionResult> UpdateStatus(int jobId,bool QA_Done=false)
        //{
        //    Response response = new Response();
        //    if (QA_Done)
        //    {
        //       response = await _jobService.UpdateStatus(jobId,JobStatusEnum.QADone, Convert.ToInt32(_authUserService.GetUserId()));
        //    }
        //    if (response.Success)
        //    {
        //        return Ok(response.data);
        //    }
        //    else
        //    {
        //        return StatusCode((int)response.ErrorType, response.ErrorMessage);
        //    }
        //}


        [HttpGet("GetLegHistory")]
        public async Task<IActionResult> GetLegHistory(int jobId)
        {
            Response<LegHistoryDto> response = await _jobService.GetLegHistory(jobId);
            if (response.Success)
            {

                return Ok(response.Objects);
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
        [HttpPost("{id}/WayPoint /AddDelete")]
        public async Task<IActionResult> AddDeleteWayPoint(int id, [FromBody] List<WayPointDto> wayPoints)
        {
            var response = await _jobService.WayPointAddDelete(id, wayPoints);
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
            Response<TrailerDto> response = await _jobService.TrailerPostPutAsync(trailer, _googleMapSettings.ApiKey, Convert.ToInt32(_authUserService.GetUserId()));
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
        [HttpDelete("Trailer/Delete")]
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
