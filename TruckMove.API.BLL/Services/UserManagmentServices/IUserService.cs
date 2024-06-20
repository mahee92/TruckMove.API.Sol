
using TruckMove.API.BLL.Helper;
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
        Task<Response> AddRoles(int id, List<int> roles);

        Task<Response<RoleDto>> GetRolesByUser(int id);
        Task<Response<UserOutputDto>> Auth(LoginDto loginModel);

        bool IsDriver(List<RoleDto> Roles);
    }
}