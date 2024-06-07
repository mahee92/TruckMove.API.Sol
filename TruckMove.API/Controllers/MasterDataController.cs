using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Services;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.Controllers.PrimaryControllers;
using TruckMove.API.DAL.Repositories.Primary;

using TruckMove.API.Settings;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.Controllers
{
    public class MasterDataController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMasterDataService _masterdataService;



        public MasterDataController(ILogger<UserController> logger, IMasterDataService masterdataService)
        {
            _logger = logger;
            _masterdataService = masterdataService;

        }
        [HttpGet("/GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _masterdataService.GetRolesAsync();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpGet("/GetDrivers")]
        public async Task<IActionResult> GetDrivers()
        {

            var response = await _masterdataService.GetUsersByRoleAsync(RoleEnum.Drivers);
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpGet("/GetOPSManagers")]
        public async Task<IActionResult> GetOPSManagers()
        {

            var response = await _masterdataService.GetUsersByRoleAsync(RoleEnum.OpsManager);
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
    }
}
