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
        public static Company ConvertToCompany(CompanyModel companyModel)
        {
            Company company = new Company();
            company.CompanyId = companyModel.CompanyId;
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

        public static CompanyModel ConvertToCompanyModel(Company company)
        {
            CompanyModel companyModel = new CompanyModel();
            companyModel.CompanyId = company.CompanyId;
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
        public static List<Company> ConvertToCompanies(List<CompanyModel> companyModels)
        {
            List<Company> companies = new List<Company>();
            foreach (var companyModel in companyModels)
            {
                companies.Add(ConvertToCompany(companyModel));
            }
            return companies;
        }
    }
}
