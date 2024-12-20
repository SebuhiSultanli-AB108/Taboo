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
            CreateMap<Word, WordGetDTO>()
              .ForMember(l => l.BannedWords, d => d.MapFrom(t =>
             t.BannedWords.Select(bw => bw.Text).ToList()));
            CreateMap<WordUpdateDTO, Word>().ReverseMap();

            CreateMap<string, BannedWord>();
        }
    }
}
