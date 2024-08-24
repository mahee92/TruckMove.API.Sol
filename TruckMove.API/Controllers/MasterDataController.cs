using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Services;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.Controllers.PrimaryControllers;
using TruckMove.API.DAL.Repositories.PrimaryRepositories;

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

            var roles = new List<RoleEnum> { RoleEnum.Driver };
            var response = await _masterdataService.GetUsersByRoleAsync(roles);
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
            var roles = new List<RoleEnum> { RoleEnum.OpsManager };
            var response = await _masterdataService.GetUsersByRoleAsync(roles);
           
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
        [HttpGet("/GetAssignees")]
        public async Task<IActionResult> GetAssignees()
        {
            var roles = new List<RoleEnum> { RoleEnum.OpsManager };
            var response = await _masterdataService.GetUsersByRoleAsync(roles);

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

        [HttpGet("/GetHookupTypes")]
        public async Task<IActionResult> HookupTypes()
        {
            var response = await _masterdataService.GetAllHookupTypes();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
               
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpGet("/GetPublicTransportTypes")]
        public async Task<IActionResult> PublicTransportTypes()
        {
            var response = await _masterdataService.GetAllPublicTransportTypes();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpGet("/TaskStatuses")]
        public async Task<IActionResult> TaskStatuses()
        {
            var response = await _masterdataService.GetAllTaskStatus();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpGet("/JobStatuses")]
        public async Task<IActionResult> JobStatuses()
        {
            var response = await _masterdataService.GetAllJobStatus();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {

                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
    }
}
