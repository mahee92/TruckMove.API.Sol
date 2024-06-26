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
using System.Runtime.InteropServices;
using static TruckMove.API.DAL.MasterData.MasterData;


namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Job> _repository;
        private readonly IRepository<JobContact> _repositoryJobContact;
    
        private readonly IJobRepository _jobRepository;
        private readonly IRepository<Vehicle> _repositoryVehicle;
        private readonly IRepository<Note> _repositoryNote;
        private readonly IRepository<VehicleImage> _repositoryVehicleImage;
        private readonly IRepository<PreDepartureChecklist> _repositorypreDepartureChecklist;

        public JobService(IMapper mapper,IRepository<Job> repository, IJobRepository jobRepository, IRepository<JobContact> repositoryJobContact, IRepository<Vehicle> repositoryVehicle, IRepository<Note> repositoryNote, IRepository<VehicleImage> repositoryVehicleImage, IRepository<PreDepartureChecklist> preDepartureChecklist)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositoryJobContact = repositoryJobContact;
            _repositoryVehicle = repositoryVehicle;
            _repositoryNote = repositoryNote;
            _repositoryVehicleImage = repositoryVehicleImage;
            _repositorypreDepartureChecklist = preDepartureChecklist;
        }
        public int DetermineJobStatus(JobDto job)
        {
            //JobOutPutDTO
            if (job.CompanyId > 0 &&
                !string.IsNullOrWhiteSpace(job.PickupLocation) &&
                !string.IsNullOrWhiteSpace(job.DropOfLocation) &&
                job.VehicleId.HasValue &&
                job.Driver.HasValue)
            {
                return (int)JobStatusEnum.Booked;
            }

            return (int)JobStatusEnum.Planned;
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
                    Job.Status = DetermineJobStatus(job);
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
                    res.Status = DetermineJobStatus(job);
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
                
                var job = await _repository.GetWithNestedIncludesAsync(id,"JobContacts.Contact", "Company", "VehicleNavigation.Notes", "VehicleNavigation.VehicleImages", "WayPoints");

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
                    if(job.VehicleNavigation != null && job.VehicleNavigation.Notes != null)
                    {
                        response.Object.Vehicle.Notes = job.VehicleNavigation.Notes.Select(jc => _mapper.Map<NoteDto>(jc)).ToList();
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
        
        #region Contact
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
        #endregion

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
                    
                    response.Object = _mapper.Map<VehicleDto>(res);

                    // Update job 
                    var job = await _repository.GetAsync(vehicle.JobId);
                    if (job != null)
                    {
                        job.VehicleId = res.Id;
                        job.Status = DetermineJobStatus(_mapper.Map<JobDto>(job));
                       
                        await _repository.UpdateAsync(job);
                    }
                    response.Success = true;

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

        public async Task<Response<NoteDto>> NotePostPutAsync(NoteDto note, int userId)
        {
            Response<NoteDto> response = new Response<NoteDto>();
            try
            {

                if (note.Id == 0)
                {
                    Note newNote = _mapper.Map<Note>(note);

                    newNote.CreatedDate = DateTime.Now;
                    newNote.CreatedById = userId;
                    
                    var res = await _repositoryNote.AddAsync(newNote);
                    response.Success = true;
                    response.Object = _mapper.Map<NoteDto>(res);
                }
                else
                {
                    var existingNote = await _repositoryNote.GetAsync(note.Id);

                    if (existingNote == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<NoteDto, Note> updater = new ObjectUpdater<NoteDto, Note>();
                        var res = updater.Map(note, existingNote);
                        res.CreatedDate = existingNote.CreatedDate;
                        res.CreatedById = existingNote.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatednote = await _repositoryNote.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<NoteDto>(updatednote);
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
                    await _repositoryNote.DeleteAsync(id);
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

        public IQueryable<MobileJobDto> GetAllAsync(int driverId)
        {
            var jobs= _jobRepository.GetAllAsync(driverId);
            return jobs.ProjectTo<MobileJobDto>(_mapper.ConfigurationProvider); 
        }
        
        #endregion

        #region WayPoint

        public async Task<Response<WayPointDto>> WayPointAddDelete(List<WayPointDto> wayPoints)
        {
            Response<WayPointDto> response = new Response<WayPointDto>();
            try
            {
                  
                List<WayPoint> existingWayPoints = await _jobRepository.GetWayPointsByJobId(wayPoints[0].JobId);
                if (existingWayPoints.Count > 0)
                {
                    await _jobRepository.DeleteWaypointsByIdsAsync(existingWayPoints.Select(x => x.Id).ToList());
                }

                List<WayPoint> newWayPoints = CreateWayPointList(wayPoints);
                await _jobRepository.AddWaypointsRangeAsync(newWayPoints);
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

       
        public List<WayPoint> CreateWayPointList(List<WayPointDto> wayPoints)
        {
            List<WayPoint> wayPointList = new List<WayPoint>();
            foreach (var wayPoint in wayPoints)
            {
                wayPointList.Add(new WayPoint { JobId = wayPoint.JobId, Location = wayPoint.Location, Coordinates = wayPoint.Coordinates});
            }
            return wayPointList;
        }
        #endregion
        public async Task<Response<PreDepartureChecklistDto>> PreDepartureChecklistPutAsync(PreDepartureChecklistDto checkList, int userId)
        {
            Response<PreDepartureChecklistDto> response = new Response<PreDepartureChecklistDto>();
            try
            {

                if (checkList.Id == 0)
                {
                    PreDepartureChecklist newChecklist = _mapper.Map<PreDepartureChecklist>(checkList);

                    newChecklist.CreatedDate = DateTime.Now;
                    newChecklist.CreatedById = userId;

                    var res = await _repositorypreDepartureChecklist.AddAsync(newChecklist);

                    response.Object = _mapper.Map<PreDepartureChecklistDto>(res);

              
                    response.Success = true;

                }
                else
                {
                    var existingCheckList = await _repositorypreDepartureChecklist.GetAsync(checkList.Id);

                    if (existingCheckList == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<PreDepartureChecklistDto, PreDepartureChecklist> updater = new ObjectUpdater<PreDepartureChecklistDto, PreDepartureChecklist>();
                        var res = updater.Map(checkList, existingCheckList);
                        res.CreatedDate = existingCheckList.CreatedDate;
                        res.CreatedById = existingCheckList.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedVehicle = await _repositorypreDepartureChecklist.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<PreDepartureChecklistDto>(updatedVehicle);


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




        


    }
}
