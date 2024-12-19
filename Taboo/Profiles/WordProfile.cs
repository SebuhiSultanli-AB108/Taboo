using AutoMapper;
using Taboo.DTOs.Word;
using Taboo.Entities;

namespace Taboo.Profiles
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<WordCreateDTO, Word>().ReverseMap();
            CreateMap<WordGetDTO, Word>().ReverseMap();
            CreateMap<WordUpdateDTO, Word>().ReverseMap();

            CreateMap<string, BannedWord>();
        }
    }
}
