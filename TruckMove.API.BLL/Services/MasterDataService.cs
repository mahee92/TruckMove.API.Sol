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
using TaskStatus = TruckMove.API.DAL.Models.TaskStatus;
using TruckMove.API.BLL.Models.PrimaryDTOs;

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

        public async Task<Response<PublicTransportType>> GetAllPublicTransportTypes()
        {
            Response<PublicTransportType> response = new Response<PublicTransportType>();
            try
            {
                var types = await _repository.GetPublicTransportTypes();
                response.Success = true;
                if (types.Count > 0)
                {
                    response.Objects = new List<PublicTransportType>();
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
        public async Task<Response<TaskStatus>> GetAllTaskStatus()
        {
            Response<TaskStatus> response = new Response<TaskStatus>();
            try
            {
                var types = await _repository.GetAllTasStatuses(); 
                response.Success = true;
                if (types.Count > 0)
                {
                    response.Objects = new List<TaskStatus>();
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
        
        public async Task<Response<JobStatus>> GetAllJobStatus()
        {
            Response<JobStatus> response = new Response<JobStatus>();
            try
            {
                var types = await _repository.GetAllJobStatus();
                response.Success = true;
                if (types.Count > 0)
                {
                    response.Objects = new List<JobStatus>();
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

        // write a method to get all rates
        public async Task<Response<UpdateRateValueDto>> GetAllRates()
        {
            Response<UpdateRateValueDto> response = new Response<UpdateRateValueDto>();
            try
            {
                var rates = await _repository.GetAllRates();
                
                if (rates.Count > 0)
                {

                    // map rates to updateratevaluedto
                    response.Objects = new List<UpdateRateValueDto>();
                    response.Objects = _mapper.Map<List<UpdateRateValueDto>>(rates);

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
        public async Task<Response<PaymentStatus>> GetAllPaymentStatus()
        {
            Response<PaymentStatus> response = new Response<PaymentStatus>();
            try
            {
                var types = await _repository.GetAllPaymentStatus();
                response.Success = true;
                if (types.Count > 0)
                {
                    response.Objects = new List<PaymentStatus>();
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




    }
}
