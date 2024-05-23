using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories.Primary;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.UserManagmentDTO;

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

    }
}
