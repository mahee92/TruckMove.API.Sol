using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.PrimaryDTOs;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

namespace TruckMove.API.Controllers.PrimaryControllers
{
    [ApiController]
    [Route("[controller]")]
#if DEBUG
#else
     [Authorize(Roles = "Administrator)]
#endif
    public class RateController : ControllerBase
    {
        private readonly ILogger<RateController> _logger;
        private readonly IRateService _rateService;
        private readonly MySettings _mySettings;
        private readonly IAuthUserService _authUserService;

        public RateController(ILogger<RateController> logger, IRateService rateService, IOptions<MySettings> mySettings, IAuthUserService authUserService)
        {
            _logger = logger;
            _rateService = rateService;
            _mySettings = mySettings.Value;
            _authUserService = authUserService;
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateRateValueDto updateRateValueDto)
        {
            var response = await _rateService.UpdateRateValueAsync(updateRateValueDto, 1);
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
