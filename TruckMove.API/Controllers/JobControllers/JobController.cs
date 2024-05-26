﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetNextJobId()
        {
            var response = _jobService.GetNextJobId();
            if (response.Success)
            {
                return Ok(response.data);
            }
            else
            {
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] JobDto job)
        {
            job.CreatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<JobDto> response = await _jobService.AddAsync(job);
            if (response.Success)
            {
        
                return Ok(response.Object);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }







    }
}
