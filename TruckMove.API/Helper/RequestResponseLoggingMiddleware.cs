using Serilog;
using System.Diagnostics;

namespace TruckMove.API.Helper
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = Guid.NewGuid().ToString();
            var startTimestamp = DateTime.UtcNow;

            // Enable buffering for request to allow reading multiple times
            context.Request.EnableBuffering();

            // Log request details
            var requestBody = await ReadRequestBody(context.Request);
            Log.Information("Request {RequestId}: {Timestamp} - Path: {RequestPath} - Method: {RequestMethod} - Body: {RequestBody}",
                            requestId, startTimestamp, context.Request.Path, context.Request.Method, requestBody);

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var endTimestamp = DateTime.UtcNow;
                var elapsedMilliseconds = (endTimestamp - startTimestamp).TotalMilliseconds;

                // Log the response details
                Log.Information("Response {RequestId}: {Timestamp} - StatusCode: {StatusCode} - Body: {ResponseBody}",
                                requestId, endTimestamp, context.Response.StatusCode, response);
                Log.Information("Request {RequestId} processed in {ElapsedMilliseconds} ms",
                                requestId, elapsedMilliseconds);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.Body.Position = 0;
            using (var reader = new StreamReader(request.Body, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body;
            }
        }
    }
}
