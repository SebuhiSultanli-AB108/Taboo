using Taboo.DTOs.Game;
using Taboo.DTOs.Word;
using Taboo.Entities;

namespace Taboo.Services.Abstracts;

public interface IGameService
{
    Task<Guid> AddAsync(GameCreateDTO dto);
    Task<WordForGameDTO> StartAsync(Guid id);
    Task<Game> GetCurrentStatus(Guid id);
    Task<WordForGameDTO> PassAsync(Guid id);
    Task<WordForGameDTO> SuccessAsync(Guid id);
    Task<WordForGameDTO> WrongAsync(Guid id);
    Task EndAsync(Guid id);
}