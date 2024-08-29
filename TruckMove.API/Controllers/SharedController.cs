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

        [HttpPost("ImageAndAttachments/Upload")]
        public async Task<IActionResult> Upload([FromForm] FileUpload fileUpload, bool? IsVehicle, bool? IsTrailer, bool? IsCompany, bool? IsContact, bool? IsPermitAndPlateAttachment, bool? IsAccomodation, bool? Ispurchase, bool? IsPublicTransport)
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
                else if (IsPermitAndPlateAttachment == true)
                {
                    filepath = Meta.TRAILER_ATTACHMENT_PATH;
                }
                else if (IsAccomodation == true)
                {
                    filepath = Meta.ACCOMODATION_ATTACHMENT_PATH;
                }
                else if (Ispurchase == true)
                {
                    filepath = Meta.PURCHASE_ATTACHMENT_PATH;
                }
                else if (IsPublicTransport == true)
                {
                    filepath = Meta.TRANSPORT_ATTACHMENT_PATH;
                }

                var fileUrl = await FileUploderUtil.UploadImage(_mySettings.FileLocation, fileUpload.file, filepath, Request.Scheme, Request.Host);
                return Ok(fileUrl);

            }
            catch (Exception ex)
            {
                return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException);

            }

        }

        [HttpPost("ImageAndAttachments/UploadMutiple")]
        public async Task<IActionResult> UploadMutiple([FromForm] MultipleFileUpload fileUpload, bool? IsCheckList)
        {
            if (fileUpload == null || fileUpload.files == null || !fileUpload.files.Any())
            {
                return StatusCode((int)ErrorCode.fileNotFound, ErrorMessages.FileNotFound);
            }
            try
            {
                var fileUrls = new List<string>();
                string filepath = "";
                if (IsCheckList == true)
                {
                    filepath = Meta.CHECKLIST_IMG_PATH;
                }

                foreach (var file in fileUpload.files)
                {
                    var fileUrl = await FileUploderUtil.UploadImage(_mySettings.FileLocation, file, filepath, Request.Scheme, Request.Host);
                    fileUrls.Add(fileUrl);
                }

                return Ok(fileUrls);
            }
            catch (Exception ex)
            {
                return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
