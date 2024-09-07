using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Reflection;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.TaskDTOs;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.Helper;
using TruckMove.API.Settings;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.Controllers.JobControllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Driver")]
    public class MobileController : Controller
    {
        private readonly IAuthUserService _authUserService;
        private readonly IJobService _jobService;
        private readonly MySettings _mySettings;
        private readonly IJobTaskService _jobTaskService;

        private readonly GoogleMapSettings _googleMapSettings;

        public MobileController(IAuthUserService authUserService, IJobService jobService, IOptions<MySettings> mySettings,IOptions<GoogleMapSettings> googleMapSettings, IJobTaskService jobtaskService)
        {

            _authUserService = authUserService;
            _jobService = jobService;
            _mySettings = mySettings.Value;
            _googleMapSettings = googleMapSettings.Value;
            _jobTaskService = jobtaskService;

        }
        [HttpGet("/Odata/Job/Get")]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            //string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiZHJpdmVyQGV4YW1wbGUuY29tIiwibmFtZWlkIjoiMjEiLCJyb2xlIjoiRHJpdmVyIiwibmJmIjoxNzE5MzM0NTIyLCJleHAiOjE3MTkzMzgxMjIsImlhdCI6MTcxOTMzNDUyMiwiaXNzIjoiaHR0cHM6Ly92dG10cnVja21vdmUuYXBpLmRldi5yaXZlcmluYS5kaWdpdGFsLyIsImF1ZCI6Imh0dHBzOi8vdnRtdHJ1Y2ttb3ZlLmFwaS5kZXYucml2ZXJpbmEuZGlnaXRhbC8ifQ.qMI46lgenS0kKwDsYf8HIew_R-IzgSIrT713Dl1m60";
            //await JobApiClient.SetJwtToken();
            //string result = await JobApiClient.ApiCallAsync();

            var query = _jobService.GetAllAsync(Convert.ToInt32(_authUserService.GetUserId()));
            var count = query.Count();          
            return Ok(query);
        }


        #region DepartureCheck
        [HttpGet("/DepartureCheck/PreDepartureChecklistFields")]
        public ActionResult<IEnumerable<FieldInfomation>> GetPreDepartureChecklistFields()
        {
            var fieldInfos = typeof(ChecklistDto).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(prop => new FieldInfomation
                {
                    DisplayName = GetDisplayName(prop),
                    Name = prop.Name,
                    Type = prop.PropertyType.Name
                })
                .ToList();

            return Ok(fieldInfos);
        }


        private string GetDisplayName(PropertyInfo prop)
        {
            var displayNameAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
            return displayNameAttr != null ? displayNameAttr.DisplayName : prop.Name;
        }

        [HttpPost("CheckList/PostPut")]
        [ValidateDriverChange]
        public async Task<IActionResult> PostPutAsync([FromHeader(Name = "JobId")] int JobId,[FromBody] ChecklistDto checkList)
        {
            Response<ChecklistDto> response = await _jobService.ChecklistPutAsync(checkList, Convert.ToInt32(_authUserService.GetUserId()));
           
            
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

        #region Leg
        [HttpPost("Leg/Post")]
        [ValidateDriverChange]
        public async Task<IActionResult> PostAsync([FromHeader(Name = "JobId")] int JobId, [FromBody] LegDto leg)
        {
            Response<LegDto> response = await _jobService.LegPostPutAsync(leg, _googleMapSettings.ApiKey, Convert.ToInt32(_authUserService.GetUserId()));
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

        [HttpPost("Purchase/PostPut")]
        [ValidateDriverChange]
        public async Task<IActionResult> PurchasePostPutAsync([FromHeader(Name = "JobId")] int JobId, [FromBody] PurchaseDto purchase)
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
       
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int jobId, bool delayOccurred = false, bool Stoped = false)
        {
            Response response = new Response();
           
            if (delayOccurred)
            {
                response = await _jobService.UpdateStatus(jobId, JobStatusEnum.Delayed, Convert.ToInt32(_authUserService.GetUserId()));
            }
            else if (Stoped)
            {
                response = await _jobService.UpdateStatus(jobId, JobStatusEnum.Stopped, Convert.ToInt32(_authUserService.GetUserId()));
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
