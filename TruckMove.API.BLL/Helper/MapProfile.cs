using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Helper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // You can add any specific mappings if needed here
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
    //public class UserProfile : Profile
    //{
    //    public UserProfile()
    //    {
    //        CreateMap<UserInputDto, User>()
    //            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
    //            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)) // or any default value
    //            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
    //            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
    //            .ForMember(dest => dest.CreatedCompanies, opt => opt.Ignore())
    //            .ForMember(dest => dest.UpdatedCompanies, opt => opt.Ignore())
    //            .ForMember(dest => dest.CreatedContacts, opt => opt.Ignore())
    //            .ForMember(dest => dest.UpdatedContacts, opt => opt.Ignore())
    //            .ForMember(dest => dest.CreatedRoles, opt => opt.Ignore())
    //            .ForMember(dest => dest.UpdatedRoles, opt => opt.Ignore())
    //            .ForMember(dest => dest.UserRoles, opt => opt.Ignore());
    //    }
    //}
}
