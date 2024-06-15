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
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.VehicleDTOs;



namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Job> _repository;
        private readonly IRepository<JobContact> _repositoryJobContact;
        private readonly IJobRepository _jobRepository;
        private readonly IRepository<Vehicle> _repositoryVehicle;

        public JobService(IMapper mapper,IRepository<Job> repository, IJobRepository jobRepository, IRepository<JobContact> repositoryJobContact, IRepository<Vehicle> repositoryVehicle)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositoryJobContact = repositoryJobContact;
            _repositoryVehicle = repositoryVehicle;
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

        public async Task<Response<JobOutPutDTO>> GetAsync(int id)
        {
            Response<JobOutPutDTO> response = new Response<JobOutPutDTO>();
            try
            {
                
                var job = await _repository.GetWithNestedIncludesAsync(id,"JobContacts.Contact", "Company");

                if (job == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    
                    response.Object = _mapper.Map<JobOutPutDTO>(job);

                    response.Object.Company = _mapper.Map<CompanyDto>(job.Company);
                    response.Object.Contacts = new List<ContactDto>();
                    response.Object.Contacts = job.JobContacts.Select(jc => _mapper.Map<ContactDto>(jc.Contact)).ToList();
                    response.Success = true;
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

        public async Task<Response> ContactAddDelete(int id, List<int> contacts)
        {
            Response response = new Response();
            try
            {
                var jobs = await _jobRepository.GetJobContactsByJobId(id);
                if (jobs.Count > 0)
                {
                    await _repositoryJobContact.DeleteByIdsAsync(jobs.Select(x => x.Id).ToList());
                }
                List<JobContact> newcontacts = CreateContactList(id, contacts);                
                await _repositoryJobContact.AddRangeAsync(newcontacts);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public List<JobContact> CreateContactList(int jobId, List<int> contacts)
        {
            List<JobContact> jobContacts = new List<JobContact>();
            foreach (var contact in contacts.Where(x=>x>0))
            {
                jobContacts.Add(new JobContact { JobId = jobId, ContactId = contact });
            }
            return jobContacts;
        }

        
        #region Vehicle
        public async Task<Response<VehicleDTO>> VehiclePostPutAsync(VehicleDTO vehicle, int userId)
        {
            Response<VehicleDTO> response = new Response<VehicleDTO>();
            try
            {
               
                if (vehicle.Id == 0)
                {
                    Vehicle newvehicle = _mapper.Map<Vehicle>(vehicle);

                    newvehicle.CreatedDate = DateTime.Now;
                    newvehicle.CreatedById = userId;

                    var res = await _repositoryVehicle.AddAsync(newvehicle);
                    response.Success = true;
                    response.Object = _mapper.Map<VehicleDTO>(res);
                }
                else
                {
                    var existingVehicle = await _repositoryVehicle.GetAsync(vehicle.Id);

                    if(existingVehicle==null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<VehicleDTO, Vehicle> updater = new ObjectUpdater<VehicleDTO, Vehicle>();
                        var res = updater.Map(vehicle, existingVehicle);
                        res.CreatedDate = existingVehicle.CreatedDate;
                        res.CreatedById = existingVehicle.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedVehicle = await _repositoryVehicle.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<VehicleDTO>(updatedVehicle);

                       
                    }
                   

                }

                return response;

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
        #endregion
    }
}
