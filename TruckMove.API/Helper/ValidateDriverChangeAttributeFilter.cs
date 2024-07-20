using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TruckMove.API.BLL.Services.JobServices;

namespace TruckMove.API.Helper
{

    public class ValidateDriverChangeAttributeFilter : IAsyncActionFilter
    {
        private readonly IAuthUserService _authUserService;

        public ValidateDriverChangeAttributeFilter(IAuthUserService authUserService)
        {
            _authUserService = authUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get driver ID from the authentication service
            int driverId = Convert.ToInt32(_authUserService.GetUserId());

            // Retrieve JobId from the request header
            if (context.HttpContext.Request.Headers.TryGetValue("JobId", out var jobIdValues))
            {
                var jobId = jobIdValues.FirstOrDefault();
                if (int.TryParse(jobId, out int jobIdInt))
                {
                    var jobService = context.HttpContext.RequestServices.GetService<IJobTaskService>();

                    if (jobService == null)
                    {

                        context.Result = new BadRequestObjectResult("Invalid driver for this job.");
                        return;
                    }
                    if (!await jobService.IsDriverValidForJob(jobIdInt, driverId))
                    {
                        context.Result = new BadRequestObjectResult("Invalid driver for this job.");
                        return;
                    }

                }
                else
                {
                    context.Result = new BadRequestObjectResult("Invalid driver for this job.");
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid driver for this job.");
                return;
            }

            await next();
        }
    }
}
