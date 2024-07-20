using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.TaskDTOs;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.DAL.Models;
using TaskStatus = TruckMove.API.DAL.Models.TaskStatus;

namespace TruckMove.API.BLL.Helper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Job, MobileJobDto>()
             .ForMember(dest => dest.VehicleNavigation, opt => opt.MapFrom(src => src.VehicleNavigation))
             .ForMember(dest => dest.Checklists, opt => opt.MapFrom(src => src.Checklists))
             .ForMember(dest => dest.Legs, opt => opt.MapFrom(src => src.Legs))
             .ForMember(dest => dest.WayPoints, opt => opt.MapFrom(src => src.WayPoints))
             .ForMember(dest => dest.Trailers, opt => opt.MapFrom(src => src.Trailers)             
             );
            CreateMap<Checklist, ChecklistDto>();
            CreateMap<ChecklistDto, Checklist>()
             .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes)
             );
            CreateMap<Vehicle, VehicleDto>();

            CreateMap<TaskStatus, TaskStatusDto>();
            CreateMap<TaskStatusDto, TaskStatus>();
            CreateMap<User, UserDto>();
            //
            CreateMap<PermitsAndPlate, PermitsAndPlateOutputDto>()
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
            .ForMember(dest => dest.AssigneeNavigation, opt => opt.MapFrom(src => src.AssigneeNavigation))
            .ForMember(dest => dest.StatusNavigation, opt => opt.MapFrom(src => src.StatusNavigation)
            );
            CreateMap<Accommodation, AccommodationOutputDto>()
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
            .ForMember(dest => dest.AssigneeNavigation, opt => opt.MapFrom(src => src.AssigneeNavigation))
            .ForMember(dest => dest.StatusNavigation, opt => opt.MapFrom(src => src.StatusNavigation))
            .ForMember(dest => dest.DriverNavigation, opt => opt.MapFrom(src => src.DriverNavigation)
            );
            CreateMap<LegStatus, LegStatusDto>();
            CreateMap<Leg, LegHistoryDto>()
                .ForMember(dest => dest.StatusNavigation, opt => opt.MapFrom(src => src.StatusNavigation)
            );

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
