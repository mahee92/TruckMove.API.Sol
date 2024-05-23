using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;
using TruckMove.API.DAL.Repositories.Primary;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TruckMove.API.BLL.Services.Primary
{

    public class CompanyService : ICompanyService
    {


        private readonly IRepository<CompanyModel> _companyRepository;
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public CompanyService(IRepository<CompanyModel> repository, IMapper mapper, IContactRepository contactRepository)
        {
         
            _companyRepository = repository;
            _mapper = mapper;
            _contactRepository = contactRepository;
        }


        public async Task<Response<CompanyDto>> GetAsync(int id)
        {
            Response<CompanyDto> response = new Response<CompanyDto>();
            try
            {
                // get only isactive companies


                var company = await _companyRepository.GetAsync(id);

                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    response.Success = true;
                    response.Object = _mapper.Map<CompanyDto>(company); 
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;


        }

        public async Task<bool> ValidateCompanyById(int id)
        {
            Response<CompanyDto> response = await GetAsync(id);
            if(response.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<Response<CompanyDtoUpdate>> UpdateAsync(CompanyDtoUpdate updatedcompany)
        {
            Response<CompanyDtoUpdate> response = new Response<CompanyDtoUpdate>();
            try
            {
                var company = await _companyRepository.GetAsync(updatedcompany.Id);
                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {


                    //var res  = _mapper.Map<CompanyModel>(updatedcompany);
                    ObjectUpdater<CompanyDtoUpdate, CompanyModel> updater = new ObjectUpdater<CompanyDtoUpdate, CompanyModel>();
                    var res = updater.Map(updatedcompany, company);
                    res.LastModifiedDate= DateTime.Now;

                        await _companyRepository.UpdateAsync(res);
                        response.Success = true;
                        //response.UpdatedObject = CompanyConvertor.ConvertToCompany(res);
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }          
            return response;

        }

        
        public async Task<Response<CompanyDto>> AddAsync(CompanyDto company)
        {
            Response<CompanyDto> response = new Response<CompanyDto>();
            try
            {
                var companyModel = _mapper.Map<CompanyModel>(company);
                companyModel.CreatedDate = DateTime.Now;
                var res = await _companyRepository.AddAsync(companyModel);
                response.Success = true;
                response.Object = _mapper.Map<CompanyDto>(res);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<Response<CompanyDto>> DeleteAsync(int id)
        {
            Response<CompanyDto> response = new Response<CompanyDto>();
            try
            {

                var company = await _companyRepository.GetAsync(id);

                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    company.IsActive = false;
                    await _companyRepository.DeleteAsync(company);
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

        
        public async Task<Response<CompanyDto>> GetAllAsync()
        {
            Response<CompanyDto> response = new Response<CompanyDto>();
            try
            {
                var companies = await _companyRepository.GetAllAsync();
                response.Success = true;
                if (companies.Count > 0)
                {
                    
                    response.Objects = new List<CompanyDto>();
                    response.Objects.AddRange(_mapper.Map<List<CompanyDto>>(companies));
                }

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
      
            return response;
        }

        public async Task UpdateCompanyPartialAsync(int id, JsonPatchDocument<CompanyDtoUpdate> patchDoc)
        {
            var company = await _companyRepository.GetAsync(id);
            if (company == null)
            {
                throw new KeyNotFoundException("Company not found");
            }

            var updateModel = new CompanyDtoUpdate();
            patchDoc.ApplyTo(updateModel);

            // Map and apply the update model to the entity using AutoMapper
            var patchedCompany = _mapper.Map(updateModel, company);
            await _companyRepository.UpdateAsync(patchedCompany);
        }

        public async Task<Response<ContactDto>> GetContactsByCompany(int companyId)
        {
            Response<ContactDto> response = new Response<ContactDto>();
            try
            {
                var companyExits = await ValidateCompanyById(companyId);
                if (!companyExits)
                {
                    response.Success = false;
                    response.ErrorType = ErrorCode.NotFound;
                    response.ErrorMessage = ErrorMessages.NotFound + " (Company Id : " + companyId + ")";

                }
                else
                {
                    var contacts = await _contactRepository.GetContactsByCompany(companyId);
                    response.Success = true;
                    if (contacts.Count > 0)
                    {
                        response.Objects = new List<ContactDto>();
                        response.Objects.AddRange(_mapper.Map<List<ContactDto>>(contacts));
                    }
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