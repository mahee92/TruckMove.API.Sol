using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Services.JobServices;
using TruckMove.API.Helper;
using TruckMove.API.Settings;

namespace TruckMove.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SharedController :  ControllerBase
    {
        private readonly MySettings _mySettings;

        public SharedController(IOptions<MySettings> mySettings)
        {
         
            _mySettings = mySettings.Value;

        }

        [HttpPost("Image/Upload")]
        public async Task<IActionResult> Upload([FromForm] FileUpload fileUpload, bool? IsVehicle, bool? IsTrailer, bool IsCompany, bool IsContact)
        {
            if (fileUpload == null || fileUpload.file == null || fileUpload.file.Length == 0)
            {
                return StatusCode((int)ErrorCode.fileNotFound, ErrorMessages.FileNotFound);
            }
            try
            {
                string filepath = "";
                if (IsVehicle == true)
                {
                    filepath = Meta.VEHICLE_IMG_PATH;
                }
                else if (IsTrailer == true)
                {
                    filepath = Meta.TRAILER_IMG_PATH;
                }
                else if (IsCompany == true)
                {
                    filepath = Meta.COMPANY_IMG_PATH;
                }
                else if (IsContact == true)
                {
                    filepath = Meta.CONTACT_IMG_PATH;
                }

                var fileUrl = await FileUploderUtil.UploadImage(_mySettings.FileLocation, fileUpload, filepath, Request.Scheme, Request.Host);
                return Ok(fileUrl);

            }
            catch (Exception ex)
            {
                return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException);

            }

        }
    }
}
