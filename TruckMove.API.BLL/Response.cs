namespace TruckMove.API.BLL
{
    public class Response<T>
    {
        public bool Success { get; set; }

        public T Object { get; set; }
        public List<T> Objects { get; set; }

        public ErrorCode ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
    public enum ErrorCode
    {
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500,
        dbError = 600
        // Add more error codes as needed
    }
}