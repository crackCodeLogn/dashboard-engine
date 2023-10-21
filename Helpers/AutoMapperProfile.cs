using AutoMapper;
using engine.Dto;
using engine.Models;

namespace engine.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Mode, ModeDto>().ReverseMap();
        CreateMap<Subject, SubjectDto>().ReverseMap();
    }
}