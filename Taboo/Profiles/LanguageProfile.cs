using AutoMapper;
using Taboo.DTOs.Language;
using Taboo.Entities;

namespace Taboo.Profiles;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<LanguageCreateDTO, Language>()
            .ForMember(l => l.Icon, d => d.MapFrom(t => t.IconUrl))
            .ReverseMap();
        CreateMap<Language, LanguageGetDTO>()
            .ForMember(l => l.IconUrl, d => d.MapFrom(t => t.Icon));
        CreateMap<Language, LanguageUpdateDTO>()
            .ReverseMap();
    }
}
