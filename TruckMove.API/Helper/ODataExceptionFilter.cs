using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TruckMove.API.Helper
{
    public class ODataExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(new
            {
                error = new
                {
                    message = context.Exception.Message,
                    details = context.Exception.StackTrace
                }
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

}
