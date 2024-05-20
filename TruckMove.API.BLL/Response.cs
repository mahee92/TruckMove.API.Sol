using TruckMove.API.BLL.Models.Primary;

namespace TruckMove.API.BLL
{
    //public class Response<T>
    //{
    //    public bool Success { get; set; }

    //    public T Object { get; set; }
    //    public List<T> Objects { get; set; }

    //    public ErrorCode ErrorType { get; set; }
    //    public string ErrorMessage { get; set; }

    //    public static implicit operator Response<T>(Response<CompanyDto> v)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class Response
    {
        public bool Success { get; set; }
        public ErrorCode ErrorType { get; set; }
        public string? ErrorMessage { get; set; }

       
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
        fileNotFound=300,
        alreadyExists = 409,
    }
}