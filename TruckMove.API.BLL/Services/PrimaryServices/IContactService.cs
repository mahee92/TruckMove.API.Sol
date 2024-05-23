using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Helper;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Services.PrimaryServices
{
    public interface IContactService
    {

        Task<Response<ContactDto>> AddAsync(ContactDto company);
        Task<Response<ContactDto>> GetAsync(int id);
        Task<Response<ContactUpdateDto>> UpdateAsync(ContactUpdateDto updatedCompany);

        Task<Response<ContactDto>> DeleteAsync(int id);
        Task<Response<ContactDto>> GetAllAsync();
    }
}
