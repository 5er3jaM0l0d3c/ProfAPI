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
;
        }
    }
}
