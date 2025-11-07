using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Mappings
{
    public class SaleProfile: Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleRequest, Sale>();
            CreateMap<Sale, SaleDTO>();
            CreateMap<SaleDTO, Sale>()
                .ForMember(dest => dest.PaymentTypeId, opt => opt.MapFrom(src => src.PaymentType != null ? src.PaymentType.Id : 0))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product != null ? src.Product.Id : 0))
                .ForMember(dest => dest.MachineId, opt => opt.MapFrom(src => src.Machine != null ? src.Machine.Id : 0));
        }
    }
}
