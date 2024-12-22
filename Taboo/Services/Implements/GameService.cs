using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Taboo.DAL;
using Taboo.DTOs.Game;
using Taboo.Entities;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements;
public class GameService(IMapper _mapper, TabooDbContext _context, IMemoryCache _cache) : IGameService
{
    static string _cacheKey;
    public async Task<Guid> AddAsync(GameCreateDTO dto)
    {
        var entity = _mapper.Map<Game>(dto);
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        _cacheKey = $"GameStatus_{entity.Id}";
        return entity.Id;
    }

    public async Task StartAsync(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        //TODO:NOtFoundException yaz
        if (entity is null) throw new Exception();
        //TODO:GamealreadyOverexception yaz
        if (entity.Score is not null) throw new Exception();
        //GameStatusDto gameStats = new()
        //{
        //    SuccessAnswer = entity.SuccessAnswer,
        //    Score = entity.Score ?? 0,
        //    WrongAnswer = entity.WrongAnswer
        //};
        GameStatusDto gameStats = new() { SuccessAnswer = 7, Score = 7, WrongAnswer = 7 };
        _setCache(gameStats);
    }

    public async Task<Game> GetCurrentStatus(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        if (entity is null) throw new Exception();
        var data = _getCache();
        if (data is null) throw new Exception();
        //TODO: new exception for cache is null
        _mapper.Map(data, entity);
        //entity.SuccessAnswer = data.SuccessAnswer;
        //entity.Score = data.Score;
        //entity.WrongAnswer = data.WrongAnswer;
        return entity;
    }

    GameStatusDto _getCache()
    {
        return (GameStatusDto)_cache.Get(_cacheKey);
    }
    void _setCache(GameStatusDto dto)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(300))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60 * 50))
            .SetPriority(CacheItemPriority.Normal);
        _cache.Remove(_cacheKey);
        _cache.Set(_cacheKey, dto, cacheEntryOptions);
    }
}
