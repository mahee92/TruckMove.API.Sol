using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.ModelConvertor;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;
using TruckMove.API.DAL.Repositories;


namespace TruckMove.API.BLL.Services.Primary
{

    public class CompanyService : ICompanyService
    {


        private readonly IRepository<CompanyModel> _companyRepository;

        public CompanyService(DbContextOptions<TrukMoveLocalContext> dbContextOptions)
        {
            _companyRepository = new Repository<CompanyModel>(dbContextOptions);
        }



        public async Task<Response<Company>> GetAsync(int id)
        {
            Response<Company> response = new Response<Company>();
            try
            {
                var company = await _companyRepository.Get(id);
                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {
                    response.Success = true;
                    response.Object = CompanyConvertor.ConvertToCompany(company);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;


        }
            
        public async Task<Response<UpdateCompany>> UpdateAsync(UpdateCompany updatedcompany)
        {
            Response<UpdateCompany> response = new Response<UpdateCompany>();
            try
            {
                var company = await _companyRepository.Get(updatedcompany.CompanyId);
                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                        ObjectUpdater<UpdateCompany, CompanyModel> updater = new ObjectUpdater<UpdateCompany, CompanyModel>();
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

        
        public async Task<Response<Company>> AddAsync(Company company)
        {
            Response<Company> response = new Response<Company>();
            try
            {
                var companyModel = CompanyConvertor.ConvertToCompanyModel(company);
                companyModel.CreatedDate = DateTime.Now;

                var res = await _companyRepository.AddAsync(companyModel);
                response.Success = true;
                response.Object = CompanyConvertor.ConvertToCompany(res);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorType = ErrorCode.dbError;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<Response<Company>> DeleteAsync(int id)
        {
            Response<Company> response = new Response<Company>();
            try
            {

                var company = await _companyRepository.Get(id);

                if (company == null)
                {
                    response.Success = false;
                    response.ErrorMessage = ErrorMessages.NotFound;
                    response.ErrorType = ErrorCode.NotFound;
                }
                else
                {

                    await _companyRepository.DeleteAsync(id);
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

        
        public async Task<Response<CompanyModel>> GetAllAsync()
        {
            Response<CompanyModel> response = new Response<CompanyModel>();
            try
            {
                var companies = await _companyRepository.GetAllAsync();
                response.Success = true;
                if (companies.Count > 0)
                {
                    response.Objects = new List<CompanyModel>();
                    response.Objects.AddRange(companies);
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


    }

}