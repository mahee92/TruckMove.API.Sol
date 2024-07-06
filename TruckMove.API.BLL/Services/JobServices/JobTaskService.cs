using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.TaskDTOs;
using TruckMove.API.DAL.Repositories.JobRepositories;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Models;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.VehicleDtos;


namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobTaskService : IJobTaskService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Job> _repository;
        private readonly IJobRepository _jobRepository;
        private readonly IRepository<PermitsAndPlate> _repositorypermitsAndPlate;
        public JobTaskService(IMapper mapper, IRepository<Job> repository, IJobRepository jobRepository, IRepository<PermitsAndPlate> repositorypermitsAndPlate)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositorypermitsAndPlate = repositorypermitsAndPlate;


        }
        #region permits and plate
        public async Task<Response<PermitsAndPlateDto>> PermitsAndPlatePostPut(PermitsAndPlateDto permitsAndPlate, int userId)
        {
            Response<PermitsAndPlateDto> response = new Response<PermitsAndPlateDto>();
            try
            {

                if (permitsAndPlate.Id == 0)
                {
                    PermitsAndPlate newPermitsAndPlate = _mapper.Map<PermitsAndPlate>(permitsAndPlate);

                    newPermitsAndPlate.CreatedDate = DateTime.Now;
                    newPermitsAndPlate.CreatedById = userId;

                    var res = await _repositorypermitsAndPlate.AddAsync(newPermitsAndPlate);

                    response.Object = _mapper.Map<PermitsAndPlateDto>(res);

                    response.Success = true;

                }
                else
                {
                    var existingPermit = await _repositorypermitsAndPlate.GetAsync(permitsAndPlate.Id);

                    if (existingPermit == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<PermitsAndPlateDto, PermitsAndPlate> updater = new ObjectUpdater<PermitsAndPlateDto, PermitsAndPlate>();
                        var res = updater.Map(permitsAndPlate, existingPermit);
                        res.CreatedDate = existingPermit.CreatedDate;
                        res.CreatedById = existingPermit.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedPermit = await _repositorypermitsAndPlate.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<PermitsAndPlateDto>(updatedPermit);


                    }


                }

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        public async Task<Response> PermitsAndPlateDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                var permit = await _repositorypermitsAndPlate.GetAsync(id);

                if (permit == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    await _repositorypermitsAndPlate.DeleteAsync(id);
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

        #endregion
    }
}
