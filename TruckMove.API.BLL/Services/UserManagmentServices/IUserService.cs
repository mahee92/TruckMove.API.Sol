
using TruckMove.API.BLL.Models.UserManagmentDTO;

namespace TruckMove.API.BLL.Services.Primary
{
    public interface IUserService
    {
        Task<Response<UserOutputDto>> AddAsync(UserInputDto company);
        Task<Response<UserOutputDto>> GetAsync(int id);
        Task<Response<UserOutputDto>> UpdateAsync(UserUpdateDto updatedCompany);

        Task<Response<UserOutputDto>> DeleteAsync(int id);
        Task<Response<UserOutputDto>> GetAllAsync();
       
    }
}