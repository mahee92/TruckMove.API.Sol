namespace TruckMove.API.Helper
{
    public interface IAuthUserService
    {
        string GetUserId();
        string GetUserName();
        bool IsFromMobile();
    }
}
