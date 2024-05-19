using AutoMapper;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.ModelConvertors;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.Primary;


namespace TruckMove.API.BLL.Services.Primary
{
    public class UserService  : IUserService
    {
        private readonly IRepository<UserModel> _repository;
        private readonly IUserRepository _userRepository;



        public UserService(IRepository<UserModel> repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<Response<UserOutputDto>> GetAsync(int id)
        {
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {
                var user = await _repository.GetAsync(id);

                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    response.Success = true;
                    response.Object = UserConvertor.ConvertUser(user);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<bool> ValidateUserEmail(string email)
        {
            return await _userRepository.CheckUserEmailExits(email);

        }

        public async Task<Response<UserOutputDto>> UpdateAsync(UserUpdateDto updatedUser)
        {
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {
                bool emailChanged = (updatedUser.Email != updatedUser.NewEmail)? true : false;

                if (!await ValidateUserEmail(updatedUser.Email))
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                    return response;

                }
                if (emailChanged)
                {
                    if (await ValidateUserEmail(updatedUser.NewEmail))
                    {
                        response.Success = false;
                        response.ErrorMessage = ErrorMessages.EmailAlreadyExists;
                        response.ErrorType = ErrorCode.alreadyExists;
                        return response;
                    }
                }
               

                var user = await _userRepository.GetUserByEmail(updatedUser.Email);

                ObjectUpdater<UserOutputDto, UserModel> updater = new ObjectUpdater<UserOutputDto, UserModel>();
                var res = updater.Map(updatedUser, user);
                if(!String.IsNullOrEmpty(updatedUser.Password))
                {
                    res.PasswordHash = PasswordHelper.HashPassword(updatedUser.Password);
                }
                if (emailChanged)
                {
                    res.Email = updatedUser.NewEmail;
                }
                res.UpdatedDate = DateTime.Now;

                await _repository.UpdateAsync(res);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response<UserOutputDto>> AddAsync(UserInputDto user)
        {
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {
                if (await ValidateUserEmail(user.Email))
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.EmailAlreadyExists;
                    response.ErrorType = ErrorCode.alreadyExists;
                    return response;

                }
                {
                    var userModel = UserConvertor.ConvertUser(user);
                    userModel.PasswordHash = PasswordHelper.HashPassword(user.Password);
                    userModel.CreatedDate = DateTime.Now;

                    var res = await _repository.AddAsync(userModel);
                    response.Success = true;
                    response.Object = UserConvertor.ConvertUser(res);
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response<UserOutputDto>> DeleteAsync(int id)
        {
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {
                var user = await _repository.GetAsync(id);

                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    user.IsActive = false;
                    await _repository.DeleteAsync(user);
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response<UserOutputDto>> GetAllAsync()
        {
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {
                var users = await _repository.GetAllAsync();
                response.Success = true;
                if (users.Count > 0)
                {
                    response.Objects = new List<UserOutputDto>();
                    response.Objects.AddRange(UserConvertor.ConvertToUserList(users));
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


    }
}
