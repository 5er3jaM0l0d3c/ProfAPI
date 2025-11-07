using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;
using System.Linq;

namespace ProfServer.Application.Mappings
{
    public class MaintenanceProfile : Profile
    {
        public MaintenanceProfile() 
        {
            CreateMap<CreateMaintenanceRequest, Maintenance>();
            CreateMap<UpdateMaintenanceRequest, Maintenance>();
            CreateMap<Maintenance, MaintenanceDTO>();
            CreateMap<MaintenanceDTO, Maintenance>()
                .ForMember(dest => dest.MachineId, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Id : 0))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : 0))
                .ForMember(dest => dest.ProblemsIds, opt => opt.MapFrom(src => src.Problems != null ? src.Problems.Select(x => x.Id) : Enumerable.Empty<int>()))
                .ForMember(dest => dest.WorkDescriptionsIds, opt => opt.MapFrom(src => src.WorkDescriptiones != null ? src.WorkDescriptiones.Select(x => x.Id) : Enumerable.Empty<int>()));
        }
    }
}
