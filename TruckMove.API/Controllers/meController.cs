using Microsoft.AspNetCore.Mvc;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL;
using Microsoft.Extensions.Options;
using TruckMove.API.Settings;

namespace TruckMove.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class meController : Controller
    {
        
        private readonly MySettings _mySettings;

        public meController(IOptions<MySettings> mySettings)
        {
            
            _mySettings = mySettings.Value;
        }
        [HttpGet]
        public async Task<IActionResult> me()
        {

            return Ok(_mySettings);
        }
    }
}
