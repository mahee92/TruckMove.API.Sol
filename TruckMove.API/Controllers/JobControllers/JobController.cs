using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.Primary;
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

        public JobController(IAuthUserService authUserService,IJobService jobService)
        {
            
            _authUserService = authUserService;
            _jobService = jobService;
        }

        
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _jobService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

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


    }
}
