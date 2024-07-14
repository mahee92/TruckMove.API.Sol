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
       
        private readonly IRepository<PreDepartureChecklist> _repositorypreDepartureChecklist;
        private readonly IRepository<Image> _repositoryImage;

        private readonly IRepository<Trailer> _repositoryTrailer;

        private readonly IMasterDataRepository _masterDataRepository;

        private readonly IRepository<Leg> _repositoryLeg;
        public JobService(IMapper mapper, IRepository<Job> repository, IJobRepository jobRepository, IRepository<JobContact> repositoryJobContact, IRepository<Vehicle> repositoryVehicle, IRepository<Note> repositoryNote, IRepository<Image> repositoryImage, IRepository<PreDepartureChecklist> preDepartureChecklist, IRepository<Trailer> repositoryTrailer, IRepository<Leg> repositoryLeg, IMasterDataRepository masterDataRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositoryJobContact = repositoryJobContact;
            _repositoryVehicle = repositoryVehicle;
            _repositoryNote = repositoryNote;
            _repositoryImage = repositoryImage;
            _repositorypreDepartureChecklist = preDepartureChecklist;
            _repositoryTrailer = repositoryTrailer;
            _masterDataRepository = masterDataRepository;
            _repositoryLeg = repositoryLeg;
        }
        #region Job
        public JobStatusEnum DetermineJobStatus(JobDto job,Job? existingJob=null)
        {
            if(existingJob!=null && existingJob.Status > (int)JobStatusEnum.ReadyForPickup)
            {
                return (JobStatusEnum)Enum.Parse(typeof(JobStatusEnum), existingJob.Id.ToString());
            }
            if (job.CompanyId > 0 &&
                !string.IsNullOrWhiteSpace(job.PickupLocation) &&
                !string.IsNullOrWhiteSpace(job.DropOfLocation) &&
                job.VehicleId.HasValue &&
                job.Driver.HasValue &&
                job.PickupDate != null)
            {
                if (job.PickupDate.Value.Date <= DateTime.Now.Date)
                {
                    return JobStatusEnum.ReadyForPickup;
                }
                return JobStatusEnum.Booked;
            }

            return JobStatusEnum.Planned;
        }
        public async Task<Response<JobDto>> PostPutAsync(JobDto job, int userId)
        {
            Response<JobDto> response = new Response<JobDto>();
            try
            {
            //    if(job.PickupDate!=null)
            //    {

            //        job.PickupDate= job.PickupDate?.AddDays(1) ?? DateTime.Now.AddDays(1);
            //    }
            //    if (job.EstimatedDeliveryDate != null)
            //    {

            //        job.EstimatedDeliveryDate = job.EstimatedDeliveryDate?.AddDays(1) ?? DateTime.Now.AddDays(1);
            //    }
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


                    JobStatusEnum status = DetermineJobStatus(job);
                    Job.Status = (int)status;
                   
                    var res = await _repository.AddAsync(Job);
                    response.Success = true;
                    response.Object = _mapper.Map<JobDto>(res);

                    //var resStatus = await _masterDataRepository.GetJobStatus((int)status);
                    response.Object.JobStatus = status.ToString();
                }
                else
                {
                    ObjectUpdater<JobDto, Job> updater = new ObjectUpdater<JobDto, Job>();
                    job.VehicleId = existingJob.VehicleId;
                    var res = updater.Map(job, existingJob);
                    res.CreatedDate = existingJob.CreatedDate;
                    res.CreatedById = existingJob.CreatedById;
                    res.LastModifiedDate = DateTime.Now;
                    res.UpdatedById = userId;
                   
                    JobStatusEnum status = DetermineJobStatus(job, existingJob);
                    res.Status = (int)status;

                    var updatedJob = await _repository.UpdateAsync(res);
                    response.Object = _mapper.Map<JobDto>(updatedJob);

                    //var resStatus = await _masterDataRepository.GetJobStatus((int)status);
                    response.Object.JobStatus = status.ToString();
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

        public async Task<Response> GetNextJobId()
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


                var job = await _repository.GetWithNestedIncludesAsync(id, "JobContacts.Contact",
                                                                            "Company",
                                                                            "VehicleNavigation.Notes",
                                                                            "VehicleNavigation.Images",
                                                                            "Trailers.Images",
                                                                            "Trailers.Notes",
                                                                            "WayPoints",
                                                                            "PermitsAndPlates.Attachments",
                                                                            "PermitsAndPlates.Notes",
                                                                            "Accommodations.Attachments",
                                                                            "Accommodations.Notes");

                if (job == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    response.Object = _mapper.Map<JobOutPutDTO>(job);

                    response.Object.Contacts = new List<ContactDto>();
                    response.Object.Contacts = job.JobContacts.Select(jc => _mapper.Map<ContactDto>(jc.Contact)).ToList();

                    response.Object.JobStatus = ((JobStatusEnum)job.Status).ToString();


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
        #endregion


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
            Vehicle resvVehicle = new Vehicle();
            try
            {
               
                if (vehicle.Id == 0)
                {
                    Vehicle newvehicle = _mapper.Map<Vehicle>(vehicle);

                    newvehicle.CreatedDate = DateTime.Now;
                    newvehicle.CreatedById = userId;

                    resvVehicle = await _repositoryVehicle.AddAsync(newvehicle);
                    
                    response.Object = _mapper.Map<VehicleDto>(resvVehicle);

                
                    
                    response.Success = true;

                }
                else
                {
                    resvVehicle = await _repositoryVehicle.GetAsync(vehicle.Id);

                    if(resvVehicle == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                        return response;
                    }
                    else
                    {
                        ObjectUpdater<VehicleDto, Vehicle> updater = new ObjectUpdater<VehicleDto, Vehicle>();
                        var res = updater.Map(vehicle, resvVehicle);
                        res.CreatedDate = resvVehicle.CreatedDate;
                        res.CreatedById = resvVehicle.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedVehicle = await _repositoryVehicle.UpdateAsync(res);

                        
                        response.Success = true;
                        response.Object = _mapper.Map<VehicleDto>(updatedVehicle);

                       
                    }
                   

                }
                // Update job 
                var job = await _repository.GetAsync(resvVehicle.JobId);

                if (job != null)
                {
                    job.VehicleId = resvVehicle.Id;

                    JobStatusEnum status = DetermineJobStatus(_mapper.Map<JobDto>(job), job);
                    job.Status = (int)status;

                    await _repository.UpdateAsync(job);

                    response.Object.JobStatus = status.ToString();
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
        
        #region Shared
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

        public async Task<Response> NoteDeleteAsync(int id)
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

        public async Task<Response<ImageDto>> ImagePostAsync(ImageDto image, int userId)
        {
            Response<ImageDto> response = new Response<ImageDto>();
            try
            {
                var newImage = _mapper.Map<Image>(image);
                newImage.CreatedDate = DateTime.Now;
                newImage.CreatedById = userId;
                var res = await _repositoryImage.AddAsync(newImage);
                response.Success = true;
                response.Object = _mapper.Map<ImageDto>(res);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<Response> ImageDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                await _repositoryImage.DeleteAsync(id);
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
        public IQueryable<MobileJobDto> GetAllAsync(int driverId)
        {
            var jobs = _jobRepository.GetAllAsync(driverId);
            return jobs.ProjectTo<MobileJobDto>(_mapper.ConfigurationProvider);
        }
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
                    
                   // newChecklist.Notes = new List<Note>();
                   // HandleNotes(checkList, newChecklist);
                    var res = await _repositorypreDepartureChecklist.AddAsync(newChecklist);

                    response.Object = _mapper.Map<PreDepartureChecklistDto>(res);


                    response.Success = true;

                }
                else
                {
                   // var existingCheckList = await _repositorypreDepartureChecklist.GetAsync(checkList.Id);
                    var existingCheckList = await _repositorypreDepartureChecklist.GetWithNestedIncludesAsync(checkList.Id, "Notes");

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

                      //  ObjectUpdater<NoteDto, Note> noteUpdater = new ObjectUpdater<NoteDto, Note>();
                        //var noteRes = updater.Map(checkList, existingCheckList);

                        res.CreatedDate = existingCheckList.CreatedDate;
                        res.CreatedById = existingCheckList.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                       // HandleNotes(checkList, existingCheckList);
                        var updatedVehicle = await _repositorypreDepartureChecklist.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<PreDepartureChecklistDto>(updatedVehicle);

                        
                    }


                }

                ChangeJobStatus(checkList.JobId, (int)JobStatusEnum.PreDepartureChecked);
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

        //public void HandleNotes(PreDepartureChecklistDto checkListdto,PreDepartureChecklist checkList)
        //{
            
        //    foreach (var noteDto in checkListdto.Notes)
        //    {
        //        var note = _mapper.Map<Note>(noteDto);
        //        if (note.Id == 0)
        //        {
        //            checkList.Notes.Add(note); // New note
        //        }
        //        else
        //        {
        //            var existingNote = checkList.Notes.FirstOrDefault(n => n.Id == note.Id);
        //            if (existingNote != null)
        //            {
        //                _mapper.Map(noteDto, existingNote); // Update existing note
        //            }
        //        }
        //    }

        //    // Remove deleted notes
        //    var updatedNoteIds = checkListdto.Notes.Select(n => n.Id).ToList();
        //    var notesToRemove = checkList.Notes.Where(n => !updatedNoteIds.Contains(n.Id)).ToList();
        //    foreach (var note in notesToRemove)
        //    {
        //        checkList.Notes.Remove(note);
        //    }
        //}
       
        public async void ChangeJobStatus(int jobId, int status)
        {
            var existingJob = await _repository.GetAsync(jobId);
            if (existingJob != null)
            {
                existingJob.Status = status;
                await _repository.UpdateAsync(existingJob);
            }

        }
        public async Task<Response<LegDto>> LegPostPutAsync(LegDto leg, string apiKey, int userId)
        {
            Response<LegDto> response = new Response<LegDto>();
            try
            {
                if (leg.Acknowledged)
                {


                    if (leg.Id == 0)
                    {
                        // validate for any ongoing leg

                        Leg newLeg = _mapper.Map<Leg>(leg);

                        newLeg.DriverId = userId;
                        newLeg.LegNumber = await GetNextLegNumber(leg.JobId);
                        newLeg.Status = (int)LegStatusEnum.InProgress;
                        newLeg.Variance = (int)VariancesEnum._default;
                        newLeg.StartTime = DateTime.Now;

                        //  

                        newLeg.CreatedDate = DateTime.Now;
                        newLeg.CreatedById = userId;

                        //  newLeg.Job.Status = (int)JobStatusEnum.InProgress;

                        var res = await _repositoryLeg.AddAsync(newLeg);
                        response.Object = _mapper.Map<LegDto>(res);
                        response.Success = true;


                        ChangeJobStatus(leg.JobId, (int)JobStatusEnum.InProgress);



                    }
                    else
                    {
                        var existingLeg = await _repositoryLeg.GetAsync(leg.Id);

                        if (existingLeg == null)
                        {
                            response.Success = false;
                            response.ErrorType = ErrorCode.NotFound;
                            response.ErrorMessage = ErrorMessages.NotFound;
                        }
                        else if (existingLeg.Status != (int)LegStatusEnum.InProgress)
                        {
                            response.Success = false;
                            response.ErrorType = ErrorCode.NotFound;
                            response.ErrorMessage = ErrorMessages.NotFound;
                        }
                        else
                        {
                            existingLeg.EndLocation = leg.EndLocation;
                            existingLeg.EndTime = DateTime.Now;
                            try
                            {
                                decimal distance = await GoogleMapsHelper.GetDistanceAsync(existingLeg.StartLocation, leg.EndLocation, apiKey);
                                existingLeg.TotalDistance = Convert.ToDouble(distance);
                            }
                            catch (Exception ex)
                            {
                                existingLeg.TotalDistance = -1;
                            }

                            existingLeg.Status = (int)LegStatusEnum.Completed;
                            existingLeg.LastModifiedDate = DateTime.Now;
                            existingLeg.UpdatedById = userId;


                            var updatedLeg = await _repositoryLeg.UpdateAsync(existingLeg);
                            response.Success = true;
                            // updatedLeg.Acknowledgement = true;
                            response.Object = _mapper.Map<LegDto>(updatedLeg);

                            if (leg.IsCompleted)
                            {
                                ChangeJobStatus(leg.JobId, (int)JobStatusEnum.Arrived);
                            }
                            else
                            {
                                ChangeJobStatus(leg.JobId, (int)JobStatusEnum.Stopped);
                            }



                        }


                    }

                    if (response.Success)
                    {
                        await _jobRepository.Acknowledge(response.Object.Id, response.Object.JobId);
                    }
                }
                else
                {
                    response.Success = false;
                    response.ErrorType = ErrorCode.AchknowledgeError;
                    response.ErrorMessage = ErrorMessages.AchknowledgeError;
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
        private async Task<int> GetNextLegNumber(int jobId)
        {
            return await _jobRepository.GetNextLegNumber(jobId);
        }

        #endregion

        #region Trailer
        public async Task<Response<TrailerDto>> TrailerPostPutAsync(TrailerDto trailer, int userId)
        {
            Response<TrailerDto> response = new Response<TrailerDto>();
            try
            {

                if (trailer.Id == 0)
                {
                    Trailer newtrailer = _mapper.Map<Trailer>(trailer);

                    newtrailer.CreatedDate = DateTime.Now;
                    newtrailer.CreatedById = userId;

                    var res = await _repositoryTrailer.AddAsync(newtrailer);

                    response.Object = _mapper.Map<TrailerDto>(res);

                   
                    response.Success = true;

                }
                else
                {
                    var existingtrailer = await _repositoryTrailer.GetAsync(trailer.Id);

                    if (existingtrailer == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<TrailerDto, Trailer> updater = new ObjectUpdater<TrailerDto, Trailer>();
                        var res = updater.Map(trailer, existingtrailer);
                        res.CreatedDate = existingtrailer.CreatedDate;
                        res.CreatedById = existingtrailer.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedTrailer = await _repositoryTrailer.UpdateAsync(res);
                        response.Success = true;
                        response.Object = _mapper.Map<TrailerDto>(updatedTrailer);


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
        public async Task<Response> TrailerDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                var company = await _repositoryTrailer.GetAsync(id);

                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                  
                    await _repositoryTrailer.DeleteAsync(id);
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
