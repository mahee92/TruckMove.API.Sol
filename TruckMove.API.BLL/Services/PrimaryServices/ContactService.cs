using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.ModelConvertor;
using TruckMove.API.BLL.Models.ModelConvertors;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.BLL.Services.PrimaryServices
{
    public class ContactService : IContactService
    {
        private readonly IRepository<ContactModel> _contactRepository;
        private readonly ICompanyService _companyService;

        public ContactService(IRepository<ContactModel> repository, ICompanyService companyService)
        {
            _contactRepository = repository;
            _companyService = companyService;
         

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
                    var ContactModel = ContactConvertor.ConvertToContactModel(contact);
                    ContactModel.CreatedDate = DateTime.Now;

                    var res = await _contactRepository.AddAsync(ContactModel);

                    response.Success = true;
                    response.Object = ContactConvertor.ConvertToContacDto(res);
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

                var contact = await _contactRepository.GetAsync(id);

                if (contact == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    contact.IsActive = false;
                    await _contactRepository.DeleteAsync(contact);
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
                var contacts = await _contactRepository.GetAllWithIncludesAsync(contact => contact.Company);
                response.Success = true;
                if (contacts.Count > 0)
                {
                    var ContactDtos = ContactConvertor.ConvertToContacts(contacts);
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
                var contact = await _contactRepository.GetAsync(id);
                if (contact == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    response.Success = true;
                    response.Object = ContactConvertor.ConvertToContacDto(contact);
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
                var contact = await _contactRepository.GetAsync(updatedContact.Id);
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
                    res.CreatedDate = DateTime.Now;

                    await _contactRepository.UpdateAsync(res);
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
