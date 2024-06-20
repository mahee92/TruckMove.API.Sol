using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Helper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Job, MobileJobDto>()
             .ForMember(dest => dest.VehicleNavigation, opt => opt.MapFrom(src => src.VehicleNavigation)); // Map Vehicle
            CreateMap<Vehicle, VehicleDto>();
        }

        public void CreateGenericMap<TSource, TDestination>()
        {
            var map = CreateMap<TSource, TDestination>();

            var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                  .Select(p => p.Name)
                                                  .ToHashSet();

            var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in destinationProperties)
            {
                if (!sourceProperties.Contains(property.Name))
                {
                    map.ForMember(property.Name, opt => opt.Ignore());
                }
            }
        }
    }
 
}
