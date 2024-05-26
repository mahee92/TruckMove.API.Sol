using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TruckMove.API.BLL.Models.Primary;

namespace TruckMove.API.BLL.Helper
{

    public class Response
    {
        public bool Success { get; set; }
        public ErrorCode ErrorType { get; set; }
        public string? ErrorMessage { get; set; }

        public string? data { get; set; }


    }
    public class Response<T> : Response where T : class
    {
        public T? Object { get; set; }
        public List<T>? Objects { get; set; }

}
    public enum ErrorCode
    {
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500,
        dbError = 600,
        fileNotFound = 300,
        alreadyExists = 409,
        invalidLogin = 401,
        validationError = 422
    }
}