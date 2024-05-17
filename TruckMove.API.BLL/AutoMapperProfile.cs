using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyDtoUpdate, CompanyModel>();
        }
    }
}
