using AutoMapper;
using engine.Dto;
using engine.Models;

namespace engine.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Mode, ModeDto>()
        .ForMember(p => p.Name, opt => opt.MapFrom(src => src.SessionMode))
        .ReverseMap();
        CreateMap<Subject, SubjectDto>()
        .ForMember(p => p.Name, opt => opt.MapFrom(src => src.SessionSubject))
        .ReverseMap();
        CreateMap<Session, SessionDto>();
        CreateMap<SessionData, SessionDataDto>();
        CreateMap<ExpiryData, ExpiryDataDto>();
    }
}