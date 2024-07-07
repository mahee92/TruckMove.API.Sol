using Serilog;
using System.Diagnostics;
using System.Text.Json;

namespace TruckMove.API.Helper
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _sensitiveKeywords = { "Password", "jwtToken", "secret" }; // Add more sensitive keywords as needed

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var requestId = Guid.NewGuid().ToString();
                var startTimestamp = DateTime.UtcNow;

                // Enable buffering for request to allow reading multiple times
                context.Request.EnableBuffering();

                // Read request body and mask sensitive data if necessary
                var requestBody = await ReadAndMaskRequestBody(context.Request);

                Log.Information("--------------------Start-------------------------------------");
                Log.Information("Request {RequestId}: {Timestamp} - Path: {RequestPath} - Method: {RequestMethod} - Body: {RequestBody}",
                                requestId, startTimestamp, context.Request.Path, context.Request.Method, requestBody);

                var originalBodyStream = context.Response.Body;
                //using (var responseBody = new MemoryStream())
                //{
                //    //context.Response.Body = responseBody;

                    await _next(context);

                //    //context.Response.Body.Seek(0, SeekOrigin.Begin);
                //    //var response = await ReadAndMaskResponseBody(context.Response);
                //    //context.Response.Body.Seek(0, SeekOrigin.Begin);

                //    var endTimestamp = DateTime.UtcNow;
                //    var elapsedMilliseconds = (endTimestamp - startTimestamp).TotalMilliseconds;

                //    // Log the response details
                //    //Log.Information("Response {RequestId}: {Timestamp} - StatusCode: {StatusCode} - Body: {ResponseBody}",
                //    //                requestId, endTimestamp, context.Response.StatusCode, response);
                //    Log.Information("Request {RequestId} processed in {ElapsedMilliseconds} ms",
                //                    requestId, elapsedMilliseconds);
                    Log.Information("--------------------End-------------------------------------");

                //   // await responseBody.CopyToAsync(originalBodyStream);
                //}
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unhandled exception has occurred while processing the request");
                await _next(context);
            }
            
        }

        private async Task<string> ReadAndMaskRequestBody(HttpRequest request)
        {
            if (request.ContentType != null && request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            {
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    request.Body.Position = 0;
                    return MaskSensitiveData(body, _sensitiveKeywords);
                }
            }
            return string.Empty;
        }

        private async Task<string> ReadAndMaskResponseBody(HttpResponse response)
        {
            if (response.ContentType != null && response.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(response.Body, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    return MaskSensitiveData(body, _sensitiveKeywords);
                }
            }
            return string.Empty;
        }

        private string MaskSensitiveData(string body, string[] sensitiveKeywords)
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(body);
                var root = jsonDocument.RootElement.Clone();
                var jsonObject = new Dictionary<string, object>();

                foreach (var property in root.EnumerateObject())
                {
                    if (Array.Exists(sensitiveKeywords, keyword => property.Name.Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        jsonObject[property.Name] = "<masked>";
                    }
                    else
                    {
                        jsonObject[property.Name] = property.Value.GetRawText();
                    }
                }

                return JsonSerializer.Serialize(jsonObject);
            }
            catch (JsonException)
            {
                // If parsing fails, return the original body
            }

            return body;
        }
    }
}
