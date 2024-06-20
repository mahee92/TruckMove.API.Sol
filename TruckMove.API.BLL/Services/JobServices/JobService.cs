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
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Models.VehicleDTOs;
using AutoMapper.QueryableExtensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;

namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Job> _repository;
        private readonly IRepository<JobContact> _repositoryJobContact;
        private readonly IJobRepository _jobRepository;
        private readonly IRepository<Vehicle> _repositoryVehicle;
        private readonly IRepository<VehicleNote> _repositoryVehicleNote;
        private readonly IRepository<VehicleImage> _repositoryVehicleImage;

        public JobService(IMapper mapper,IRepository<Job> repository, IJobRepository jobRepository, IRepository<JobContact> repositoryJobContact, IRepository<Vehicle> repositoryVehicle,IRepository<VehicleNote> repositoryVehicleNote, IRepository<VehicleImage> repositoryVehicleImage)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositoryJobContact = repositoryJobContact;
            _repositoryVehicle = repositoryVehicle;
            _repositoryVehicleNote = repositoryVehicleNote;
            _repositoryVehicleImage = repositoryVehicleImage;
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
                
                var job = await _repository.GetWithNestedIncludesAsync(id,"JobContacts.Contact", "Company", "VehicleNavigation.VehicleNotes", "VehicleNavigation.VehicleImages");
                //
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
        
                    response.Object.Vehicle = _mapper.Map<VehicleOutputDto>(job.VehicleNavigation);
                    if(job.VehicleNavigation != null && job.VehicleNavigation.VehicleNotes != null)
                    {
                        response.Object.Vehicle.VehicleNotes = job.VehicleNavigation.VehicleNotes.Select(jc => _mapper.Map<VehicleNoteDto>(jc)).ToList();
                    }

                    
                    if (job.VehicleNavigation != null && job.VehicleNavigation.VehicleImages != null)
                    {
                        response.Object.Vehicle.VehicleImages = job.VehicleNavigation.VehicleImages.Select(jc => _mapper.Map<VehicleImageDto>(jc)).ToList();
                    }


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
        //public async Task<Response<JobDto>> GetAllAsync()
        //{
        //    Response<JobDto> response = new Response<JobDto>();
        //    try
        //    {
        //        var jobs = await _repository.GetAllAsync();
        //        response.Success = true;
        //        if (jobs.Count > 0)
        //        {

        //            response.Objects = new List<JobDto>();
        //            response.Objects.AddRange(_mapper.Map<List<JobDto>>(jobs));
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
        public Response<JobOutPutDTO> GetAllAsync(int i)
        {
            Response<JobOutPutDTO> response = new Response<JobOutPutDTO>();
            try
            {
                var jobs1 = _jobRepository.GetAllAsync();
                var jobs = _jobRepository.GetAllAsync().ToList();
                response.Success = true;
                if (jobs.Count > 0)
                {

                    ////response.Objects = new List<JobDto>();
                    ////response.Objects.AddRange(_mapper.Map<List<JobDto>>(jobs));
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
        public IQueryable<MobileJobDto> GetAllAsync2(int i)
        {
            var x = _jobRepository.GetAllAsync();
            var sql = x.ToQueryString();
             var jobDtos = x.ProjectTo<MobileJobDto>(_mapper.ConfigurationProvider);
            return jobDtos;
           
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
        public async Task<Response<VehicleDto>> VehiclePostPutAsync(VehicleDto vehicle, int userId)
        {
            Response<VehicleDto> response = new Response<VehicleDto>();
            try
            {
               
                if (vehicle.Id == 0)
                {
                    Vehicle newvehicle = _mapper.Map<Vehicle>(vehicle);

                    newvehicle.CreatedDate = DateTime.Now;
                    newvehicle.CreatedById = userId;

                    var res = await _repositoryVehicle.AddAsync(newvehicle);
                    response.Success = true;
                    response.Object = _mapper.Map<VehicleDto>(res);
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
                        ObjectUpdater<VehicleDto, Vehicle> updater = new ObjectUpdater<VehicleDto, Vehicle>();
                        var res = updater.Map(vehicle, existingVehicle);
                        res.CreatedDate = existingVehicle.CreatedDate;
                        res.CreatedById = existingVehicle.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedVehicle = await _repositoryVehicle.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<VehicleDto>(updatedVehicle);

                       
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

        public async Task<Response<VehicleNoteDto>> VehicleNotePostPutAsync(VehicleNoteDto note, int userId)
        {
            Response<VehicleNoteDto> response = new Response<VehicleNoteDto>();
            try
            {

                if (note.Id == 0)
                {
                    VehicleNote newNote = _mapper.Map<VehicleNote>(note);

                    newNote.CreatedDate = DateTime.Now;
                    newNote.CreatedById = userId;

                    var res = await _repositoryVehicleNote.AddAsync(newNote);
                    response.Success = true;
                    response.Object = _mapper.Map<VehicleNoteDto>(res);
                }
                else
                {
                    var existingNote = await _repositoryVehicleNote.GetAsync(note.Id);

                    if (existingNote == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<VehicleNoteDto, VehicleNote> updater = new ObjectUpdater<VehicleNoteDto, VehicleNote>();
                        var res = updater.Map(note, existingNote);
                        res.CreatedDate = existingNote.CreatedDate;
                        res.CreatedById = existingNote.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatednote = await _repositoryVehicleNote.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<VehicleNoteDto>(updatednote);
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

        public async Task<Response> VehicleNoteDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {          
                    await _repositoryVehicleNote.DeleteAsync(id);
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

        public async Task<Response<VehicleImageDto>> VehicleImagePostAsync(VehicleImageDto image, int userId)
        {
            Response<VehicleImageDto> response = new Response<VehicleImageDto>();
            try
            {
                var newImage = _mapper.Map<VehicleImage>(image);
                newImage.CreatedDate = DateTime.Now;
                newImage.CreatedById = userId;
                var res = await _repositoryVehicleImage.AddAsync(newImage);
                response.Success = true;
                response.Object = _mapper.Map<VehicleImageDto>(res);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response> VehicleImageDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                await _repositoryVehicleImage.DeleteAsync(id);
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
        #endregion

        #region Mobile
        public Response<DriverJobStatus> GetDriverJobStaus(int driverId)
        {
            Response<DriverJobStatus> response = new Response<DriverJobStatus>();
            response.Success = true;
            response.Object.HasCurrentJob = false;
            return response;
        }

       
        //public IQueryable<Job> GetAllAsync(int driverId)
        //{
        //   //// Response<JobDto> response = new Response<JobDto>();
        //   // try
        //   // {
        //   //     //var jobs = await _jobRepository.GetAllJobsByDriverAsync(driverId, "VehicleNavigation");
        //   //     //var jobs = await _jobRepository.GetAllJobsByDriverAsync(driverId, "VehicleNavigation");
        //       var jobs = _jobRepository.GetAllAsyn();
        //    return jobs;
        //   //     //response.Success = true;
        //   //     //if (jobs.Count > 0)
        //   //     //{

        //    //     //    response.Objects = new List<JobDto>();
        //    //     //    response.Objects.AddRange(_mapper.Map<List<JobDto>>(jobs));
        //    //     //}

        //    // }
        //    // catch (Exception ex)
        //    // {
        //    //     response.Success = false;
        //    //     response.ErrorType = ErrorCode.dbError;
        //    //     response.ErrorMessage = ex.Message;
        //    // }

        //    // return response;
        //}
        #endregion

    }
}
