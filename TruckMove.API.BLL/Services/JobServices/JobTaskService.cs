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
using static TruckMove.API.DAL.MasterData.MasterData;
using TruckMove.API.DAL.Repositories.PaymentRepositories;
using Microsoft.AspNetCore.Mvc;
using TruckMove.API.DAL.VMmodels;

namespace TruckMove.API.BLL.Services.JobServices
{
    public class JobTaskService : IJobTaskService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Job> _repository;
        private readonly IJobRepository _jobRepository;
        private readonly IRepository<Attachment> _repositoryAttachment;
        private readonly IRepository<PermitsAndPlate> _repositorypermitsAndPlate;
        private readonly IRepository<Accommodation> _repositoryAccommodation;
        private readonly IRepository<PublicTransport> _repositoryPublicTransport;
        private readonly IRepository<Purchase> _repositoryPurchase;
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<Delay> _repositoryDelay;
        private readonly IPaymentRepository _paymentRepository;
        public JobTaskService(IMapper mapper, IRepository<Job> repository, IJobRepository jobRepository, IRepository<PermitsAndPlate> repositorypermitsAndPlate, IRepository<Attachment> repositoryAttachment, IRepository<Accommodation> repositoryAccomadation, IRepository<PublicTransport> repositoryPublicTransport, IRepository<Purchase> repositoryPurchase, ITaskRepository taskRepository,IRepository<Delay> delay, IPaymentRepository paymentRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositorypermitsAndPlate = repositorypermitsAndPlate;
            _repositoryAttachment = repositoryAttachment;
            _repositoryAccommodation = repositoryAccomadation;
            _repositoryPublicTransport = repositoryPublicTransport;
            _repositoryPurchase = repositoryPurchase;
            _taskRepository = taskRepository;
            _repositoryDelay = delay;
            _paymentRepository = paymentRepository;


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
                    var res2 = await _repositorypermitsAndPlate.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                           "StatusNavigation"
                                                                           );

                    response.Object = _mapper.Map<PermitsAndPlateOutputDto>(res2);

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
                        var updatedPermit2 = await _repositorypermitsAndPlate.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                           "StatusNavigation"
                                                                            );
                        response.Success = true;
                        response.Object = _mapper.Map<PermitsAndPlateOutputDto>(updatedPermit2);


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

        public async Task<Response<AttachmentDto>> AttachmentPostAsync(AttachmentDto attachment, int userId)
        {
            Response<AttachmentDto> response = new Response<AttachmentDto>();
            try
            {
                var newAttachment = _mapper.Map<Attachment>(attachment);
                newAttachment.CreatedDate = DateTime.Now;
                newAttachment.CreatedById = userId;
                var res = await _repositoryAttachment.AddAsync(newAttachment);
                response.Success = true;
                response.Object = _mapper.Map<AttachmentDto>(res);

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
                await _repositoryAttachment.DeleteAsync(id);
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

        #region Accommodation
        public async Task<Response<AccommodationDto>> AccommodationPostPut(AccommodationDto accommodation, int userId)
        {
            Response<AccommodationDto> response = new Response<AccommodationDto>();
            try
            {
                if (accommodation.Id == 0)
                {
                    Accommodation newAccommodation = _mapper.Map<Accommodation>(accommodation);
                    newAccommodation.CreatedDate = DateTime.Now;
                    newAccommodation.CreatedById = userId;
                    var res = await _repositoryAccommodation.AddAsync(newAccommodation);
                    var res2 = await _repositoryAccommodation.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                        "StatusNavigation", "DriverNavigation"
                                                                        );
                    response.Object = _mapper.Map<AccommodationOutputDto>(res2);
                    response.Success = true;
                }
                else
                {
                    var existingAccommodation = await _repositoryAccommodation.GetAsync(accommodation.Id);
                    if (existingAccommodation == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<AccommodationDto, Accommodation> updater = new ObjectUpdater<AccommodationDto, Accommodation>();
                        var res = updater.Map(accommodation, existingAccommodation);
                        res.CreatedDate = existingAccommodation.CreatedDate;
                        res.CreatedById = existingAccommodation.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedAccommodation = await _repositoryAccommodation.UpdateAsync(res);
                        var res2 = await _repositoryAccommodation.GetWithNestedIncludesAsync(updatedAccommodation.Id, "AssigneeNavigation",
                                                                        "StatusNavigation", "DriverNavigation"
                                                                        );
                        response.Success = true;
                        response.Object = _mapper.Map<AccommodationOutputDto>(updatedAccommodation);
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
        
        public async Task<Response> AccommodationDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                var accommodation = await _repositoryAccommodation.GetAsync(id);

                if (accommodation == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    await _repositoryAccommodation.DeleteAsync(id);
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

        public async Task<Response<PublicTransportOutputDto>> PublicTransportPostPut(PublicTransportDto transport, int userId)
        {
            Response<PublicTransportOutputDto> response = new Response<PublicTransportOutputDto>();
            try
            {
                if (transport.Id == 0)
                {
                    PublicTransport newTransport = _mapper.Map<PublicTransport>(transport);

                    newTransport.CreatedDate = DateTime.Now;
                    newTransport.CreatedById = userId;
                    var res = await _repositoryPublicTransport.AddAsync(newTransport);
                    var res2 = await _repositoryPublicTransport.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                   "StatusNavigation", "DriverNavigation"
                                                                   );

                    response.Object = _mapper.Map<PublicTransportOutputDto>(res2);
                    response.Success = true;
                }
                else
                {
                    var existingTransport = await _repositoryPublicTransport.GetAsync(transport.Id);
                    if (existingTransport == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<PublicTransportDto, PublicTransport> updater = new ObjectUpdater<PublicTransportDto, PublicTransport>();
                        var res = updater.Map(transport, existingTransport);
                        res.CreatedDate = existingTransport.CreatedDate;
                        res.CreatedById = existingTransport.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedTransport = await _repositoryPublicTransport.UpdateAsync(res);
                        var res2 = await _repositoryPublicTransport.GetWithNestedIncludesAsync(updatedTransport.Id, "AssigneeNavigation",
                                                                   "StatusNavigation", "DriverNavigation"
                                                                   );
                        response.Success = true;
                        response.Object = _mapper.Map<PublicTransportOutputDto>(res2);
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
        #endregion
        #region PublicTransport

        public async Task<Response> PublicTransportDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                var transport = await _repositoryPublicTransport.GetAsync(id);

                if (transport == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    await _repositoryPublicTransport.DeleteAsync(id);
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

        public async Task<Response<PurchaseOutputDto>> PurchasePostPut(PurchaseDto purchase, int userId)
        {
            Response<PurchaseOutputDto> response = new Response<PurchaseOutputDto>();
            try
            {
                if (purchase.Id == 0)
                {
                    Purchase newPurchase = _mapper.Map<Purchase>(purchase);

                    newPurchase.CreatedDate = DateTime.Now;
                    newPurchase.CreatedById = userId;
                    var res = await _repositoryPurchase.AddAsync(newPurchase);
                    var res2 = await _repositoryPurchase.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                  "StatusNavigation", "DriverNavigation"
                                                                  );
                    response.Object = _mapper.Map<PurchaseOutputDto>(res2);
                    response.Success = true;
                }
                else
                {
                    var existingPurchase = await _repositoryPurchase.GetAsync(purchase.Id);
                    if (existingPurchase == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<PurchaseDto, Purchase> updater = new ObjectUpdater<PurchaseDto, Purchase>();
                        var res = updater.Map(purchase, existingPurchase);
                        res.CreatedDate = existingPurchase.CreatedDate;
                        res.CreatedById = existingPurchase.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        var updatedPurchase = await _repositoryPurchase.UpdateAsync(res);
                        var res2 = await _repositoryPurchase.GetWithNestedIncludesAsync(updatedPurchase.Id, "AssigneeNavigation",
                                                                 "StatusNavigation", "DriverNavigation"
                                                                 );
                        response.Success = true;
                        response.Object = _mapper.Map<PurchaseOutputDto>(res2);
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
        public async Task<Response> PurchaseDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                var transport = await _repositoryPurchase.GetAsync(id);

                if (transport == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    await _repositoryPurchase.DeleteAsync(id);
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

        public async Task<bool> IsDriverValidForJob(int jobId, int driverId)
        {
            string res = await _repository.GetPropertyAsync(jobId, "Driver");
            if (res != null && res == driverId.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion


        #region Delay

        public void HandleDrivers(DelayDto delayDto, Delay delay)
        {
            // Get the list of current driver IDs in the delay
            var existingDriverIds = delay.DelayDrivers.Select(dd => dd.Id).ToList();

            // Loop through the drivers in delayDto
            foreach (var delayDriverDto in delayDto.DelayDrivers)
            {
                // If it's a new driver (Id == 0), add it
                if (delayDriverDto.Id == 0)
                {
                    var newDelayDriver = new DelayDriver
                    {
                        DriverId = delayDriverDto.DriverId,
                        DelayId = delay.Id
                    };
                    delay.DelayDrivers.Add(newDelayDriver);
                }
                else
                {
                    // If an existing driver is being updated (Id != 0)
                    var existingDriver = delay.DelayDrivers.FirstOrDefault(dd => dd.Id == delayDriverDto.Id);

                    if (existingDriver != null)
                    {
                        // Update the DriverId if it has changed
                        if (existingDriver.DriverId != delayDriverDto.DriverId)
                        {
                            existingDriver.DriverId = delayDriverDto.DriverId;
                        }
                    }
                }
            }

            // Remove drivers that are not in the updated list
            var updatedDriverIds = delayDto.DelayDrivers.Select(n => n.Id).ToList();
            var driversToRemove = delay.DelayDrivers.Where(n => !updatedDriverIds.Contains(n.Id)).ToList();
            foreach (var driver in driversToRemove)
            {
                delay.DelayDrivers.Remove(driver);
            }
        }
        public async Task<Response<DelayOutputDto>> DelayPostPut(DelayDto delay, int userId)
        {
            Response<DelayOutputDto> response = new Response<DelayOutputDto>();
            try
            {
                if (delay.Id == 0)
                {
                    Delay newDelay = _mapper.Map<Delay>(delay);
                    newDelay.CreatedDate = DateTime.Now;
                    newDelay.CreatedById = userId;
                    var res = await _repositoryDelay.AddAsync(newDelay);
                 
                   var res2 = await _repositoryDelay.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                  "StatusNavigation", "DelayDrivers", "TypeNavigation"
                                                                  );
                   // HandleDrivers(delay, res2);
                    response.Object = _mapper.Map<DelayOutputDto>(res2);
                    response.Success = true;
                }
                else
                {
                    var existingDelay = await _repositoryDelay.GetWithNestedIncludesAsync(delay.Id, "DelayDrivers"
                                                                  );
                    if (existingDelay == null)
                    {
                        response.Success = false;
                        response.ErrorType = ErrorCode.NotFound;
                        response.ErrorMessage = ErrorMessages.NotFound;
                    }
                    else
                    {
                        ObjectUpdater<DelayDto, Delay> updater = new ObjectUpdater<DelayDto, Delay>();
                        var res = updater.Map(delay, existingDelay);
                        res.CreatedDate = existingDelay.CreatedDate;
                        res.CreatedById = existingDelay.CreatedById;
                        res.LastModifiedDate = DateTime.Now;
                        res.UpdatedById = userId;
                        HandleDrivers(delay, existingDelay);
                        var updatedDelay = await _repositoryDelay.UpdateAsync(res);
                        var res2 = await _repositoryDelay.GetWithNestedIncludesAsync(res.Id, "AssigneeNavigation",
                                                                  "StatusNavigation", "DelayDrivers", "TypeNavigation"
                                                                  );

                        
                        response.Success = true;
                        response.Object = _mapper.Map<DelayOutputDto>(res2);
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

        public async Task<Response> DelayDeleteAsync(int id)
        {
            Response response = new Response();
            try
            {
                var delay = await _repositoryDelay.GetAsync(id);

                if (delay == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    await _repositoryDelay.DeleteAsync(id);
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
        #region MyTasks
        //get all tasks for a user



        public async Task<Response<GraphData>> GetGraphData()
        {
            var response = new Response<GraphData>();
            try
            {
                var UpcommingJobsByPickupDate = _taskRepository.GetUpcommingJobsByPickupDate();
                var UpcomingJobsByCompany = _taskRepository.GetUpcomingJobsByCompany();
                var UpcomingJobsByDriver = _taskRepository.GetUpcomingJobsByDriver();
                var UpcomingJobsByDriverOverTime = _taskRepository.GetUpcomingJobsByDriverOverTime();
                await Task.WhenAll( UpcommingJobsByPickupDate, UpcomingJobsByCompany, UpcomingJobsByDriver, UpcomingJobsByDriverOverTime);

                var UpcommingJobsByPickupDateResult = await UpcommingJobsByPickupDate;
                var UpcomingJobsByCompanyResult = await UpcomingJobsByCompany;
                var UpcomingJobsByDriverResult = await UpcomingJobsByDriver;
                var UpcomingJobsByDriverOverTimeResult = await UpcomingJobsByDriverOverTime;

                response.Object = new GraphData();
                response.Object.UpcommingJobsByPickupDate = new Dictionary<DateTime, int>();
                if (UpcommingJobsByPickupDateResult != null && UpcommingJobsByPickupDateResult.Count > 0)
                {

                    response.Object.UpcommingJobsByPickupDate = UpcommingJobsByPickupDateResult;
                }
                response.Object.UpcomingJobsByCompany = new Dictionary<string, int>();
                if (UpcomingJobsByCompanyResult != null && UpcomingJobsByCompanyResult.Count > 0)
                {
                    response.Object.UpcomingJobsByCompany = UpcomingJobsByCompanyResult;
                }
                response.Object.UpcomingJobsByDriver = new Dictionary<string, int>();

                if (UpcomingJobsByDriverResult != null && UpcomingJobsByDriverResult.Count > 0)
                {
                    response.Object.UpcomingJobsByDriver = UpcomingJobsByDriverResult;
                }
                //
                response.Object.UpcomingJobsByDriverOverTime = new Dictionary<string, int>();
                if (UpcomingJobsByDriverOverTimeResult != null && UpcomingJobsByDriverOverTimeResult.Count > 0)
                {
                    response.Object.UpcomingJobsByDriverOverTime = UpcomingJobsByDriverOverTimeResult;
                }
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

        // create a method to getjobsbyuserid   


        public async Task<Response<JobOutPutDTO>> GetMyJobs(int userId)
        {
            var response = new Response<JobOutPutDTO>();
            try
            {
                var myJobs = await  _taskRepository.GetJobsByUserId(userId);
                
                response.Objects = new List<JobOutPutDTO>();
                response.Objects = myJobs.Select(jc => _mapper.Map<JobOutPutDTO>(jc)).ToList();
                
                response.Success = true;

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }





        public async Task<Response<MyTaskDto>> GetMyTasks(int userId)
        {
            var response = new Response<MyTaskDto>();

            try
            {
                var permitTasks = _taskRepository.GetPermitsAndPlateTasksByUserId(userId);
                var accommodationTasks = _taskRepository.GetAccommodationTasksByUserId(userId);
                var publicTransportTasks = _taskRepository.GetPublicTransportTasksByUserId(userId);
                var purchaseTasks = _taskRepository.GetPurchaseTasksByUserId(userId);
                var drivingTasks = _taskRepository.GetDrivingTasksByUserId(userId);
                var delayTasks = _taskRepository.GetDelayTasksByUserId(userId);
                //var GetJobsEligibleForPaymentQA = _paymentRepository.GetQAPendingList(userId);//_taskRepository.GetJobsEligibleForPaymentQA(userId);



                await Task.WhenAll(permitTasks, accommodationTasks, publicTransportTasks, purchaseTasks, drivingTasks,delayTasks);

               
                var permitsResult = await permitTasks;
                var accommodationsResult = await accommodationTasks;
                var publicTransportsResult = await publicTransportTasks;
                var purchasesResult = await purchaseTasks;
                var drivingResult = await drivingTasks;
                var delayResult = await delayTasks;

                //var GetJobsEligibleForPaymentQAResult = await GetJobsEligibleForPaymentQA;

                response.Object = new MyTaskDto();
                if (permitsResult != null && permitsResult.Count > 0)
                {
                    response.Object.PermitsAndPlates = new List<PermitsAndPlateOutputDto>();
                    AddMappedTasksToResponse(response.Object.PermitsAndPlates, permitsResult);
                }
                if (accommodationsResult != null && accommodationsResult.Count > 0)
                {
                    response.Object.Accommodations = new List<AccommodationOutputDto>();
                    AddMappedTasksToResponse(response.Object.Accommodations, accommodationsResult);
                }
                if (publicTransportsResult != null && publicTransportsResult.Count > 0)
                {
                    response.Object.PublicTransports = new List<PublicTransportOutputDto>();
                    AddMappedTasksToResponse(response.Object.PublicTransports, publicTransportsResult);
                }
                if (purchasesResult != null && purchasesResult.Count > 0)
                {
                    response.Object.Purchases = new List<PurchaseOutputDto>();
                    AddMappedTasksToResponse(response.Object.Purchases, purchasesResult);
                }
                if (drivingResult != null && drivingResult.Count > 0)
                {
                    response.Object.DrivingTasks = new List<Job>();
                    AddMappedTasksToResponse(response.Object.DrivingTasks, drivingResult);
                }
                if (delayResult != null && delayResult.Count > 0)
                {
                    response.Object.Delays = new List<DelayDto>();
                    AddMappedTasksToResponse(response.Object.Delays, delayResult);
                }

                 response.Object.JobsEligibleForPaymentQA = new List<DriverJobPaymentVM>();
                 var res =  _paymentRepository.GetQAPendingList(userId).ToList();
                 response.Object.JobsEligibleForPaymentQA.AddRange(res);
                  
                    
                    
                


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

        private void AddMappedTasksToResponse<TInput, TOutput>(List<TOutput> destinationList, List<TInput> sourceList)
        {
            if (sourceList != null && sourceList.Any())
            {
                destinationList.AddRange(_mapper.Map<List<TOutput>>(sourceList));
            }
        }
        #endregion
    }

}