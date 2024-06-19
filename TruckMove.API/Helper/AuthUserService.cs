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
            var k = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
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
        public string IsDriver()
        {
            var roles = _httpContextAccessor.HttpContext?.User?.Claims
           .Where(c => c.Type == ClaimTypes.Role)
           .Select(c => c.Value)
           .ToList();
            return "";
        }
    }
}
