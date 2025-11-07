using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Models;

namespace ProfServer.Application.Mappings
{
    public class Maintenance_ProblemProfile : Profile
    {
        public Maintenance_ProblemProfile()
        {
            CreateMap<CreateMaintenance_ProblemRequest, Maintenance_Problem>();
            CreateMap<UpdateMaintenance_ProblemRequest, Maintenance_Problem>();
        }
    }
}
