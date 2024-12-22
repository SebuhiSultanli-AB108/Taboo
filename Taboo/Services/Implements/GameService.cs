using AutoMapper;
using Taboo.DAL;
using Taboo.DTOs.Game;
using Taboo.Entities;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements;
public class GameService(IMapper _mapper, TabooDbContext _context) : IGameService
{
    public async Task<Guid> AddAsync(GameCreateDTO dto)
    {
        var entity = _mapper.Map<Game>(dto);
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task StartAsync(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        //TODO:NOtFoundException yaz
        if (entity is null) throw new Exception();
        //TODO:GamealreadyOverexception yaz
        if (entity.Score is not null) throw new Exception();

        //Add SkipCount, FailCount and WrongAnswer to the cash
        //hint hasentry keyword
    }

    public async Task<Game> GetCurrentStatus(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        //TODO:NOtFoundException yaz
        if (entity is null) throw new Exception();

        //get data from the cash

        //entity.SuccessAnswer = new data
        //entity.WrongAnswer = new data
        //entity.SkipCount = new data
        return entity;
    }
}
