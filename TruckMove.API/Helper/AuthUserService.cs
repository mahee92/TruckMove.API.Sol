using System.Security.Claims;
using TruckMove.API.BLL.Services.Primary;

namespace TruckMove.API.Helper
{
   
    public class AuthUserService : IAuthUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
#if DEBUG
            // This code will only run in debug mode
            return "2";
#else
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
#endif

        }

        public string GetUserName()
        {
           
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }
        public bool IsFromMobile()
        {
            if (_httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("fromMobile", out var fromMobileHeader) == true)
            {
                string fromMobile = fromMobileHeader.FirstOrDefault();
                return bool.TryParse(fromMobile, out bool isFromMobile) && isFromMobile;
            }
            return false;
        }
    }
}
