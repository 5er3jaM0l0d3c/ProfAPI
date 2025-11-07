using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.Interfaces;
using ProfServer.Models;


namespace ProfServer.Application.Mappings
{
    public class Machine_ProductProfile : Profile
    {
        public Machine_ProductProfile()
        {
            CreateMap<CreateMachine_ProductRequest, Machine_Product>();
            CreateMap<UpdateMachine_ProductRequest, Machine_Product>();
            CreateMap<Machine_Product, Machine_ProductDTO>();
            CreateMap<Machine_ProductDTO, Machine_Product>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product != null ? src.Product.Id : 0))
                .ForMember(dest => dest.MachineId, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Id : 0));
;
        }
    }
}
