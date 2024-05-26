using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.Primary;

namespace TruckMove.API.BLL.Services.PrimaryServices
{
    public class ContactService : IContactService
    {
        private readonly IRepository<ContactModel> _repository;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private IContactRepository _contactRepository;

        public ContactService(IRepository<ContactModel> repository, ICompanyService companyService, IMapper mapper,IContactRepository contactRepository)
        {
            _repository = repository;
            _companyService = companyService;
            _mapper = mapper;
            _contactRepository = contactRepository;

        }

        public async Task<Response<ContactDto>> AddAsync(ContactDto contact)
        {
            Response<ContactDto> response = new Response<ContactDto>();
            try
            {
                //Validate Company
                var companyExits = await _companyService.ValidateCompanyById(contact.CompanyId);
                if(!companyExits)
                {
                    response.Success = false;
                    response.ErrorType = ErrorCode.NotFound;
                    response.ErrorMessage = ErrorMessages.NotFound + " (Company Id : " + contact.CompanyId + ")";

                }
                else
                {

                    var ContactModel = _mapper.Map<ContactModel> (contact); 
                    ContactModel.CreatedDate = DateTime.Now;

                    var res = await _repository.AddAsync(ContactModel);

                    response.Success = true;
                    response.Object = _mapper.Map<ContactDto>(res);;
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

        public async Task<Response<ContactDto>> DeleteAsync(int id)
        {
            Response<ContactDto> response = new Response<ContactDto>();
            try
            {

                var contact = await _repository.GetAsync(id);

                if (contact == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    contact.IsActive = false;
                    await _repository.DeleteAsync(contact);
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

        public async Task<Response<ContactDto>> GetAllAsync()
        {
            
            Response<ContactDto> response = new Response<ContactDto>();
            try
            {
                //var contacts = await _contactRepository.GetAllWithIncludesAsync(contact => contact.Company);
                var contacts = await _contactRepository.GetAllAsync();
                response.Success = true;
                if (contacts.Count > 0)
                {
                   
                    var ContactDtos = _mapper.Map<List<ContactDto>>(contacts);
                    response.Objects = new List<ContactDto>();
                    
                    response.Objects.AddRange(ContactDtos);
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

        public async Task<Response<ContactDto>> GetAsync(int id)
        {
            Response<ContactDto> response = new Response<ContactDto>();
            try
            {
                //var contact = await _contactRepository.GetWithIncludesAsync(id, contact => contact.Company);
                var contact = await _repository.GetAsync(id);
                if (contact == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    response.Success = true;
                    response.Object = _mapper.Map<ContactDto>(contact); 
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;


        }

        public async Task<Response<ContactUpdateDto>> UpdateAsync(ContactUpdateDto updatedContact)
        {
            Response<ContactUpdateDto> response = new Response<ContactUpdateDto>();
            try
            {
                var contact = await _repository.GetAsync(updatedContact.Id);
                if (contact == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    ObjectUpdater<ContactUpdateDto, ContactModel> updater = new ObjectUpdater<ContactUpdateDto, ContactModel>();
                    var res = updater.Map(updatedContact, contact);
                    res.LastModifiedDate = DateTime.Now;

                    await _repository.UpdateAsync(res);
                    response.Success = true;
                    //response.UpdatedObject = CompanyConvertor.ConvertToCompany(res);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;

        }
    }
}
