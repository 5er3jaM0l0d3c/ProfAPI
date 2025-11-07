using AutoMapper;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
