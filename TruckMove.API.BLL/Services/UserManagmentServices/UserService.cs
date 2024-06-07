using AutoMapper;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.Primary;


namespace TruckMove.API.BLL.Services.Primary
{
    public class UserService  : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

    
        public UserService(IRepository<User> repository, IUserRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            
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
                    response.Object = _mapper.Map<UserOutputDto>(user); ;
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

                ObjectUpdater<UserUpdateDto, User> updater = new ObjectUpdater<UserUpdateDto, User>();
                var res = updater.Map(updatedUser, user);
              
                if(!String.IsNullOrEmpty(updatedUser.Password))
                {
                    res.PasswordHash = PasswordHelper.HashPassword(updatedUser.Password);
                }
                if (emailChanged)
                {
                    res.Email = updatedUser.NewEmail;
                }
                res.LastModifiedDate = DateTime.Now;

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
                   
                    User User = _mapper.Map<User>(user);
                    User.PasswordHash = PasswordHelper.HashPassword(user.Password);
                    User.CreatedDate = DateTime.Now;

                    var res = await _repository.AddAsync(User);
                    response.Success = true;
                    response.Object = _mapper.Map<UserOutputDto>(res);
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
                    response.Objects= _mapper.Map<List<UserOutputDto>>(users);
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

        public async Task<Response> AddRoles(int id, List<int> roles)
        {
            Response response = new Response();
            try
            {
               
                await _userRepository.AddRolesAsync(id, roles);
                response.Success = true;

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response<RoleDto>> GetRolesByUser(int id)
        {
            Response<RoleDto> response = new Response<RoleDto>();
            try
            {

               var roles = await _userRepository.GetRolesByUserId(id);
               response.Success = true;
               response.Objects = new List<RoleDto>();
               response.Objects.AddRange(_mapper.Map<List<RoleDto>>(roles));

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response<UserOutputDto>> Auth(LoginDto loginModel)
        {
            
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {
                var EmailExits = await _userRepository.CheckUserEmailExits(loginModel.UserName);
                if (!EmailExits)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.Invalidlogin;
                    response.ErrorType = ErrorCode.invalidLogin;
                }
                else
                {
                    var user = await _userRepository.GetUserByEmailWithRoles(loginModel.UserName);
                    if (PasswordHelper.VerifyPassword(user.PasswordHash,loginModel.Password))
                    {
                        response.Success = true;


                        response.Object = _mapper.Map<UserOutputDto>(user);
                        
                        if (user.UserRoles!=null)
                        {
                            var roles = user.UserRoles.Select(ur => ur.Role).ToList();
                            response.Object.Roles = new List<RoleDto>();
                            response.Object.Roles.AddRange(_mapper.Map<List<RoleDto>>(roles));
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.ErrorMessage = ErrorMessages.Invalidlogin;
                        response.ErrorType = ErrorCode.invalidLogin;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorCode.dbError;
            }
            return response;
        }
    }
}
