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
        private readonly IRepository<Attachment> _repositoryAttachment;
        private readonly IRepository<PermitsAndPlate> _repositorypermitsAndPlate;
        private readonly IRepository<Accommodation> _repositoryAccommodation;
        private readonly IRepository<PublicTransport> _repositoryPublicTransport;
        private readonly IRepository<Purchase> _repositoryPurchase;
        public JobTaskService(IMapper mapper, IRepository<Job> repository, IJobRepository jobRepository, IRepository<PermitsAndPlate> repositorypermitsAndPlate, IRepository<Attachment> repositoryAttachment, IRepository<Accommodation> repositoryAccomadation, IRepository<PublicTransport> repositoryPublicTransport, IRepository<Purchase> repositoryPurchase)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
            _repositorypermitsAndPlate = repositorypermitsAndPlate;
            _repositoryAttachment = repositoryAttachment;
            _repositoryAccommodation = repositoryAccomadation;
            _repositoryPublicTransport = repositoryPublicTransport;
            _repositoryPurchase = repositoryPurchase;


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

        public async Task<Response<PublicTransportDto>> PublicTransportPostPut(PublicTransportDto transport, int userId)
        {
            Response<PublicTransportDto> response = new Response<PublicTransportDto>();
            try
            {
                if (transport.Id == 0)
                {
                    PublicTransport newTransport = _mapper.Map<PublicTransport>(transport);

                    newTransport.CreatedDate = DateTime.Now;
                    newTransport.CreatedById = userId;
                    var res = await _repositoryPublicTransport.AddAsync(newTransport);
                    response.Object = _mapper.Map<PublicTransportDto>(res);
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
                        response.Success = true;
                        response.Object = _mapper.Map<PublicTransportDto>(updatedTransport);
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

        public async Task<Response<PurchaseDto>> PurchasePostPut(PurchaseDto purchase, int userId)
        {
            Response<PurchaseDto> response = new Response<PurchaseDto>();
            try
            {
                if (purchase.Id == 0)
                {
                    Purchase newPurchase = _mapper.Map<Purchase>(purchase);

                    newPurchase.CreatedDate = DateTime.Now;
                    newPurchase.CreatedById = userId;
                    var res = await _repositoryPurchase.AddAsync(newPurchase);
                    response.Object = _mapper.Map<PurchaseDto>(res);
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
                        response.Success = true;
                        response.Object = _mapper.Map<PurchaseDto>(updatedPurchase);
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
        #endregion

    }

}