using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.ModelConvertor
{
    public static class CompanyConvertor
    {
        public static CompanyDto ConvertToCompany(CompanyModel companyModel)
        {
            CompanyDto company = new CompanyDto();
            company.Id = companyModel.Id;
            company.CompanyName = companyModel.CompanyName;
            company.PhoneNumber = companyModel.PhoneNumber;
            company.CompanyAddress = companyModel.CompanyAddress;
            company.PrimaryEmail = companyModel.PrimaryEmail;
            company.AccountsEmail = companyModel.AccountsEmail;
            company.CompanyStreetAddress = companyModel.CompanyStreetAddress;
            company.CompanyABN = companyModel.CompanyABN;
            //company.CreatedDate = companyModel.CreatedDate;
            return company;
        }

        public static CompanyModel ConvertToCompanyModel(CompanyDto company)
        {
            CompanyModel companyModel = new CompanyModel();
            companyModel.Id = company.Id;
            companyModel.CompanyName = company.CompanyName;
            companyModel.PhoneNumber = company.PhoneNumber;
            companyModel.CompanyAddress = company.CompanyAddress;
            companyModel.PrimaryEmail = company.PrimaryEmail;
            companyModel.AccountsEmail = company.AccountsEmail;
            companyModel.CompanyStreetAddress = company.CompanyStreetAddress;
            companyModel.CompanyABN = company.CompanyABN;
            //companyModel.CreatedDate = company.CreatedDate;
            return companyModel;
        }

        // create ConvertToCompanies method
        public static List<CompanyDto> ConvertToCompanies(List<CompanyModel> companyModels)
        {
            List<CompanyDto> companies = new List<CompanyDto>();
            foreach (var companyModel in companyModels)
            {
                companies.Add(ConvertToCompany(companyModel));
            }
            return companies;
        }
    }
}
