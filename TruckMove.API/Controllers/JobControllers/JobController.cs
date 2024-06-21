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
        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var response = await _jobService.GetAllAsync();
        //    if (response.Success)
        //    {
        //        return Ok(response.Objects);
        //    }
        //    else
        //    {

        //        return StatusCode((int)response.ErrorType, response.ErrorMessage);
        //    }
        //}
        //[HttpGet("Odata/GetAll")]       
        //[EnableQuery(PageSize = 10)]
        //public  IActionResult GetAllAsync()
        //{
        //    var response =  _jobService.GetAllAsync(1);
        //    if (response.Success)
        //    {
        //        return Ok(response.Objects);
        //    }
        //    else
        //    {

        //        return StatusCode((int)response.ErrorType, response.ErrorMessage);
        //    }
        //}

        //[HttpGet("Odata/GetAll")]
        //[EnableQuery(PageSize = 10)]
        //public IActionResult Get()
        //{
            
        //    return Ok(_jobService.GetAllAsync2(1));
        //}

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

        [HttpPost("VehicleNote/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] VehicleNoteDto note)
        {
            Response<VehicleNoteDto> response = await _jobService.VehicleNotePostPutAsync(note, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("VehicleNote/Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response response = await _jobService.VehicleNoteDeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
               
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }


        [HttpPost("VehicleImage/Upload")]
        public async Task<IActionResult> Upload([FromForm] FileUpload fileUpload)
        {
            if (fileUpload == null || fileUpload.file == null || fileUpload.file.Length == 0)
            {
                return StatusCode((int)ErrorCode.fileNotFound, ErrorMessages.FileNotFound);
            }
            try
            {
                var fileUrl = await FileUploderUtil.UploadImage(_mySettings.FileLocation, fileUpload, Meta.VEHICLE_IMG_PATH, Request.Scheme, Request.Host);
                return Ok(fileUrl);

            }
            catch (Exception ex)
            {
                return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException);

            }

        }

        [HttpPost("VehicleImage/Post")]
        public async Task<IActionResult> PostAsync([FromBody] VehicleImageDto image)
        {
            Response<VehicleImageDto> response = await _jobService.VehicleImagePostAsync(image, Convert.ToInt32(_authUserService.GetUserId()));
            if (response.Success)
            {

                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("VehicleImage/Delete")]
        public async Task<IActionResult> VehicleImageDeleteAsync(int id)
        {
            Response response = await _jobService.VehicleImageDeleteAsync(id);
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
      
        
        #region Mobile

        //[HttpGet("Mobile/GetDriverJobStaus")]
        //public async Task<IActionResult> GetDriverJobStaus()
        //{
        //    Response<DriverJobStatus> response = _jobService.GetDriverJobStaus(Convert.ToInt32(_authUserService.GetUserId()));

        //    if (response.Success)
        //    {
        //        return Ok(response.Object);
        //    }
        //    else
        //    {
        //        return StatusCode((int)response.ErrorType, response.ErrorMessage);
        //    }
        //}


        #endregion


    }
}
