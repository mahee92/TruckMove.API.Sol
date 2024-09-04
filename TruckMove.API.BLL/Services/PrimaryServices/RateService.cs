using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories.PrimaryRepositories;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.PrimaryDTOs;

namespace TruckMove.API.BLL.Services.PrimaryServices
{
    public class RateService : IRateService
    {
        private readonly IRepository<Rates> _rateyRepository;
        private readonly IMapper _mapper;


        public RateService(IRepository<Rates> repository, IMapper mapper)
        {

            _rateyRepository = repository;
            _mapper = mapper;

        }
        //update rate value
        public async Task<Response<UpdateRateValueDto>> UpdateRateValueAsync(UpdateRateValueDto updatedRate, int userId)
        {
            Response<UpdateRateValueDto> response = new Response<UpdateRateValueDto>();
            try
            {
                var rate = await _rateyRepository.GetAsync(updatedRate.Id,false);
                if (rate == null)
                {
                    response.Success = false;
                    response.ErrorType = ErrorCode.NotFound;
                    response.ErrorMessage = ErrorMessages.NotFound;
                }
                else
                {
                    rate.Value = updatedRate.Value;
                    rate.UpdatedById = userId;
                    var res = await _rateyRepository.UpdateAsync(rate);
                    response.Success = true;
                    response.Object = _mapper.Map<UpdateRateValueDto>(res);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.InternalServerError;
                response.ErrorMessage = ErrorMessages.InternalError;
            }
            return response;
        }
    }
}
