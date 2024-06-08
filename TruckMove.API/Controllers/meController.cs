using Microsoft.AspNetCore.Mvc;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL;
using Microsoft.Extensions.Options;
using TruckMove.API.Settings;
using TruckMove.API.Helper;

namespace TruckMove.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class meController : Controller
    {
        
        private readonly MySettings _mySettings;
        private readonly IAuthUserService _authUserService;

        public meController(IOptions<MySettings> mySettings, IAuthUserService authUserService)
        {
            
            _mySettings = mySettings.Value;
            _authUserService = authUserService;
        }
        [HttpGet]
        public async Task<IActionResult> me()
        {
            _mySettings.loggedUser = Convert.ToInt32(_authUserService.GetUserId());
            return Ok(_mySettings);
        }
    }
}
