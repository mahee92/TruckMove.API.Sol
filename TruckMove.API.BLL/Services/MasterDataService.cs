using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Services
{
    public class MasterDataService : IMasterDataService
    {
     
        private readonly IMasterDataRepository _repository;
        private readonly IMapper _mapper;


        public MasterDataService(IMasterDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public Task<List<DAL.dbFirst.HookupType>> GetAllRolesHookupTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<RoleDto>> GetRolesAsync()
        {
            Response<RoleDto> response = new Response<RoleDto>();
            try
            {
                var roles = await _repository.GetAllRoles();
                response.Success = true;
                if (roles.Count > 0)
                {
                    response.Objects = new List<RoleDto>();
                    response.Objects = _mapper.Map<List<RoleDto>>(roles);
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

        public async Task<Response<UserOutputDto>> GetUsersByRoleAsync(List<RoleEnum> roles)
        {
            
            Response<UserOutputDto> response = new Response<UserOutputDto>();
            try
            {

                List<int> roleIds = roles.Select(role => (int)role).ToList();        
                var res = await _repository.GetUsersByRolesAsync(roleIds);
                response.Success = true;
                if (res.Count > 0)
                {
                    response.Objects = new List<UserOutputDto>();
                    response.Objects = _mapper.Map<List<UserOutputDto>>(res);
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

        public async Task<Response<HookupType>> GetAllHookupTypes()
        {
            Response<HookupType> response = new Response<HookupType>();
            try
            {
                var types = await _repository.GetAllRolesHookupTypes();
                response.Success = true;
                if (types.Count > 0)
                {
                    response.Objects = new List<HookupType>();
                    response.Objects = types;
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
        //public async Task<Response<JobStatus>> GetAllHookupTypes()
        //{
        //    Response<JobStatus> response = new Response<JobStatus>();
        //    try
        //    {
        //        var types = await _repository.GetAllRolesHookupTypes();
        //        response.Success = true;
        //        if (types.Count > 0)
        //        {
        //            response.Objects = new List<HookupType>();
        //            response.Objects = types;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.ErrorType = ErrorCode.dbError;
        //        response.ErrorMessage = ex.Message;
        //    }
        //    return response;
        //}


    }
}
