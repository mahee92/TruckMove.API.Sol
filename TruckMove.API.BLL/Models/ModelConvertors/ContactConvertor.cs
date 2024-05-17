using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.ModelConvertors
{
    public class ContactConvertor
    {
        // create a method to convert ContactDto to ContactModel
        public static ContactModel ConvertToContactModel(ContactDto contact)
        {
            ContactModel contactModel = new ContactModel();
            contactModel.Id = contact.Id;
            contactModel.ContactName = contact.ContactName;
            contactModel.ContactMobileNumber = contact.ContactMobileNumber;
            contactModel.ContactLandline = contact.ContactLandline;
            contactModel.ContactsEmail = contact.ContactsEmail;
            contactModel.ContactStreetAddress = contact.ContactStreetAddress;
            contactModel.CompanyId = contact.CompanyId;
            return contactModel;
        }


        public static ContactDto ConvertToContacDto(ContactModel contact)
        {
            ContactDto ContactDto = new ContactDto();
            ContactDto.Id = contact.Id;
            ContactDto.ContactName = contact.ContactName;
            ContactDto.ContactMobileNumber = contact.ContactMobileNumber;
            ContactDto.ContactLandline = contact.ContactLandline;
            ContactDto.ContactsEmail = contact.ContactsEmail;
            ContactDto.ContactStreetAddress = contact.ContactStreetAddress;
            ContactDto.CompanyId = contact.CompanyId;
            ContactDto.CompanyName = (contact.Company != null) ? contact.Company.CompanyName : "";
            return ContactDto;
        }

        public static List<ContactDto> ConvertToContacts(List<ContactModel> contactModels)
        {
            List<ContactDto> companies = new List<ContactDto>();
            foreach (var companyModel in contactModels)
            {
                companies.Add(ConvertToContacDto(companyModel));
            }
            return companies;
        }


    }
}
