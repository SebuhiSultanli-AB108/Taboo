using AutoMapper;
using Taboo.DTOs.Language;
using Taboo.Entities;

namespace Taboo.Profiles;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<LanguageDTO, Language>()
            .ForMember(l => l.Icon, d => d.MapFrom(t => t.IconUrl));
        CreateMap<Language, LanguageGetDTO>();
    }
}
