using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Taboo.DAL;
using Taboo.DTOs.Game;
using Taboo.DTOs.Word;
using Taboo.Entities;
using Taboo.Exceptions.Game;
using Taboo.ExternalServices.Abstracts;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements;
public class GameService(IMapper _mapper, TabooDbContext _context, ICacheService _cache) : IGameService
{
    public async Task<Guid> AddAsync(GameCreateDTO dto)
    {
        var entity = _mapper.Map<Game>(dto);
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<WordForGameDTO> StartAsync(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        if (entity is null) throw new GameNotFoundException();
        if (entity.Score is not null) throw new GameAlreadyOverException();
        var words = await _context.Words
            .Where(x => x.LanguageCode == entity.LanguageCode)
            .Take(10)
            .Select(x => new WordForGameDTO
            {
                Id = x.Id,
                Text = x.Text,
                BannedWords = x.BannedWords.Select(y => y.Text).Take((int)entity.Difficulty).ToList()
            })
            .ToListAsync();
        GameStatusDto status = new GameStatusDto()
        {
            Success = 0,
            Wrong = 0,
            Pass = 0,
            MaxPassCount = (byte)entity.SkipCount,
            LanguageCode = entity.LanguageCode,
            Difficulty = entity.Difficulty,
            Words = new Stack<WordForGameDTO>(words),
            UsedWordIds = words.Select(x => x.Id).ToList(),
        };
        var currentWord = status.Words.Pop();
        await _cache.SetAsync(id.ToString(), status);
        return currentWord;
    }

    public async Task<Game> GetCurrentStatus(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        if (entity is null) throw new Exception();
        return entity;
    }

    public async Task<WordForGameDTO> PassAsync(Guid id)
    {
        var status = await _getGameStatusAsync(id);
        await _AddNewWords(status);
        if (status.Pass >= status.MaxPassCount)
            EndAsync(id);
        //end the game 
        else
        {
            status.Pass++;
            var currentWord = status.Words.Pop();
            await _cache.SetAsync(id.ToString(), status);
            return currentWord;
        }
        return null;
    }

    public async Task<WordForGameDTO> SuccessAsync(Guid id)
    {
        var status = await _getGameStatusAsync(id);
        await _AddNewWords(status);
        status.Success++;
        var currentWord = status.Words.Pop();

        await _cache.SetAsync(id.ToString(), status);
        return currentWord;
    }

    public async Task<WordForGameDTO> WrongAsync(Guid id)
    {
        var status = await _getGameStatusAsync(id);
        await _AddNewWords(status);
        status.Wrong++;
        var currentWord = status.Words.Pop();
        await _cache.SetAsync(id.ToString(), status);
        return currentWord;
    }

    public Task EndAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    async Task<GameStatusDto> _getGameStatusAsync(Guid id)
    {
        GameStatusDto status = await _cache.GetAsync<GameStatusDto>(id.ToString());
        if (status is null)
            throw new GameNotFoundException();
        return status;
    }

    async Task _AddNewWords(GameStatusDto status)
    {
        if (status.Words.Count <= 5)
        {
            var newWords = await _context.Words
                .Where(w => w.LanguageCode == status.LanguageCode && !status.UsedWordIds.Contains(w.Id))
                .Take(5)
                .Select(x => new WordForGameDTO
                {
                    Id = x.Id,
                    Text = x.Text,
                    BannedWords = x.BannedWords.Select(y => y.Text).ToList()
                })
                .ToListAsync();
            status.UsedWordIds.AddRange(newWords.Select(w => w.Id));
            newWords.ForEach(x => status.Words.Push(x));
        }
    }
}
