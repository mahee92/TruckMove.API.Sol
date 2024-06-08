using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.JobRepositories;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.JobDTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient.Server;
using TruckMove.API.DAL.Repositories.PrimaryRepositories;

namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Job> _repository;
        private readonly IJobRepository _jobRepository;

        public JobService(IMapper mapper,IRepository<Job> repository,IJobRepository jobRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
        }

        public async Task<Response<JobDto>> PostPutAsync(JobDto job,int userId)
        {
            Response<JobDto> response = new Response<JobDto>();
            try
            {

                if (!IsPossibleToAdd(job))
                {
                    response.Success = false;
                    response.ErrorType = ErrorCode.validationError;
                    response.ErrorMessage = ErrorMessages.Invalid;
                    return response;
                }


                var existingJob = await _repository.GetAsync(job.Id);



                if (existingJob == null)
                {
                    var Job = _mapper.Map<Job>(job);
                    Job.CreatedDate = DateTime.Now;
                    Job.CreatedById = userId;

                    var res = await _repository.AddAsync(Job);
                    response.Success = true;
                    response.Object = _mapper.Map<JobDto>(res);
                }
                else
                {
                    ObjectUpdater<JobDto, Job> updater = new ObjectUpdater<JobDto, Job>();
                    var res = updater.Map(job, existingJob);
                    res.CreatedDate = existingJob.CreatedDate;
                    res.CreatedById = existingJob.CreatedById;
                    res.LastModifiedDate = DateTime.Now;
                    res.UpdatedById = userId;
                    await _repository.UpdateAsync(res);
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

        public bool IsPossibleToAdd(JobDto job)
        {
            if (job.Id < 1 || job.CompanyId < 1)
            {
                return false; 
            }
            return true;
        }

        // CREATE METHOD TO GET NEXT JOB ID
        public  async Task<Response> GetNextJobId()
        {
            Response response = new Response();
            try
            {
                var id = await _jobRepository.GetNextJobId();
                response.Success = true;
                response.data = id.ToString();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.ErrorType = ErrorCode.dbError;

            }
            return response;
            
        }

        public async Task<Response<JobDto>> GetAsync(int id)
        {
            Response<JobDto> response = new Response<JobDto>();
            try
            {
                // get only isactive companies


                var job = await _repository.GetAsync(id);

                if (job == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    response.Success = true;
                    response.Object = _mapper.Map<JobDto>(job);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;


        }
        public async Task<Response<JobDto>> GetAllAsync()
        {
            Response<JobDto> response = new Response<JobDto>();
            try
            {
                var jobs = await _repository.GetAllAsync();
                response.Success = true;
                if (jobs.Count > 0)
                {

                    response.Objects = new List<JobDto>();
                    response.Objects.AddRange(_mapper.Map<List<JobDto>>(jobs));
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
