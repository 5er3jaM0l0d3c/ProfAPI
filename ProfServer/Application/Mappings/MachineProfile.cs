using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Mappings
{
    public sealed class MachineProfile : Profile
    {
        public MachineProfile()
        {
            // Machine -> MachineDTO
            CreateMap<Machine, MachineDTO>();
            CreateMap<CreateMachineRequest, Machine>();
            CreateMap<UpdateMachineRequest, Machine>();

            // MachineDTO -> Machine
            CreateMap<MachineDTO, Machine>()
                // навигационные объекты автоматически сопоставятся по имени,
                // но нужно заполнить FK поля из вложенных DTO
                .ForMember(dest => dest.PaymentTypeId, opt => opt.MapFrom(src => src.PaymentType != null ? src.PaymentType.Id : 0))
                .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Manufacturer != null ? src.Manufacturer.Id : 0))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status != null ? src.Status.Id : 0))
                .ForMember(dest => dest.ManufactureCountryId, opt => opt.MapFrom(src => src.ManufactureCountry != null ? src.ManufactureCountry.Id : 0));
        }
    }
}