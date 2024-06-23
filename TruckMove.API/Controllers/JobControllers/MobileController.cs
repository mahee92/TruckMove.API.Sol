using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

namespace TruckMove.API.Controllers.JobControllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Driver")]
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
            string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiZHJpdmVyQGV4YW1wbGUuY29tIiwibmFtZWlkIjoiMjEiLCJyb2xlIjoiRHJpdmVyIiwibmJmIjoxNzE5MDYxNDU2LCJleHAiOjE3MTkwNjUwNTYsImlhdCI6MTcxOTA2MTQ1NiwiaXNzIjoiaHR0cHM6Ly92dG10cnVja21vdmUuYXBpLmRldi5yaXZlcmluYS5kaWdpdGFsLyIsImF1ZCI6Imh0dHBzOi8vdnRtdHJ1Y2ttb3ZlLmFwaS5kZXYucml2ZXJpbmEuZGlnaXRhbC8ifQ.9UCaFADn0NwIaXrXyEx_DfW9r-tAQ1A6Offlvuy0XdU";
            await JobApiClient.SetJwtToken();
            string result = await JobApiClient.ApiCallAsync();

            var query = _jobService.GetAllAsync(Convert.ToInt32(_authUserService.GetUserId()));
            var count = query.Count();
            if (count == 0)
            {

                return Ok();
            }
            return Ok(query);
        }
    }
}
