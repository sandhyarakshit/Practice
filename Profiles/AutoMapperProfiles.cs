using AutoMapper;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SchoolApplication.DomainModels;
using SchoolApplication.Models;
using SchoolApplication.Profiles.AfterMaps;

namespace SchoolApplication.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Student, DomainModels.Student>().ReverseMap();
            CreateMap<Models.Gender, DomainModels.Gender>().ReverseMap();
            CreateMap<Models.Address, DomainModels.Address>().ReverseMap();
            CreateMap<UpdateStudentRequest, Models.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentRequest, Models.Student>().AfterMap<AddStudentRequestAfterMap>();
        }
    }
}
