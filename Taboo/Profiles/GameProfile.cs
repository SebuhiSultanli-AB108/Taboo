﻿using AutoMapper;
using Taboo.DTOs.Game;
using Taboo.Entities;

namespace Taboo.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<GameCreateDTO, Game>()
            .ForMember(x => x.Time, y => y.MapFrom(z => new TimeSpan(10000000 * z.Seconds)))
            .ForMember(x => x.BannedWordCount, y => y.MapFrom(z => (int)z.GameLevel))
            .ForMember(x => x.Difficulty, y => y.MapFrom(z => (int)z.GameLevel));
        CreateMap<GameStatusDto, Game>();
    }
}
