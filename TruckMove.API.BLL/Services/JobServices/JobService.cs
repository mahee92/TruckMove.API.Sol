﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.Job;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.JobDTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient.Server;

namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<JobModel> _repository;
        private readonly IJobRepository _jobRepository;

        public JobService(IMapper mapper,IRepository<JobModel> repository,IJobRepository jobRepository)
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
               
                
                JobModel existingJob = await _jobRepository.GetJobById(job.JobId);
                
                if (existingJob==null)
                {
                    var jobModel = _mapper.Map<JobModel>(job);
                    jobModel.CreatedDate = DateTime.Now;
                    jobModel.CreatedById = userId;
                    var res = await _repository.AddAsync(jobModel);
                    response.Success = true;
                    response.Object = _mapper.Map<JobDto>(res);
                }
                else
                {
                    ObjectUpdater<JobDto, JobModel> updater = new ObjectUpdater<JobDto, JobModel>();
                    var res = updater.Map(job, existingJob);
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
            if (job.JobId < 1 || job.CompanyId < 1)
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


    }
}
