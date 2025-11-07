using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Mappings
{
    public class Maintanence_WorkDescriptionProfile : Profile
    {
        public Maintanence_WorkDescriptionProfile()
        {
            CreateMap<CreateMaintenance_WorkDescriptionRequest, Maintenance_WorkDescription>();
            CreateMap<UpdateMaintenance_WorkDescriptionRequest, Maintenance_WorkDescription>();
        }
    }
}
