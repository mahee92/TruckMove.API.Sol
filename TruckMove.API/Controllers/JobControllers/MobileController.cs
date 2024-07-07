using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Reflection;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

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

        public MobileController(IAuthUserService authUserService, IJobService jobService, IOptions<MySettings> mySettings)
        {

            _authUserService = authUserService;
            _jobService = jobService;
            _mySettings = mySettings.Value;

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
            if (count == 0)
            {

                return Ok();
            }
            return Ok(query);
        }


        #region DepartureCheck
        [HttpGet("/DepartureCheck/PreDepartureChecklistFields")]
        public ActionResult<IEnumerable<FieldInfomation>> GetPreDepartureChecklistFields()
        {
            var fieldInfos = typeof(PreDepartureChecklistDto).GetProperties(BindingFlags.Public | BindingFlags.Instance)
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

        [HttpPost("DepartureCheck/PostPut")]
        public async Task<IActionResult> PostPutAsync([FromBody] PreDepartureChecklistDto checkList)
        {
            Response<PreDepartureChecklistDto> response = await _jobService.PreDepartureChecklistPutAsync(checkList, Convert.ToInt32(_authUserService.GetUserId()));
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
