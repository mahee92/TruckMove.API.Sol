using System.Security.Claims;

namespace TruckMove.API.Helper
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var userName = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    context.Items["UserId"] = userId;
                }

                if (!string.IsNullOrEmpty(userName))
                {
                    context.Items["UserName"] = userName;
                }
            }

            await _next(context);
        }
    }
}
