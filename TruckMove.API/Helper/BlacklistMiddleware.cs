namespace TruckMove.API.Helper
{
    public class BlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public BlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract the token from the Authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Check if the token is blacklisted
            if (token != null && TokenBlacklist.IsTokenBlacklisted(token))
            {
                // Respond with 401 Unauthorized if the token is blacklisted
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has been revoked.");
                return;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
