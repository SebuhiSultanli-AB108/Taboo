using Taboo.DTOs.Game;
using Taboo.Entities;

namespace Taboo.Services.Abstracts;

public interface IGameService
{
    Task<Guid> AddAsync(GameCreateDTO dto);
    Task StartAsync(Guid id);
    Task<Game> GetCurrentStatus(Guid id);
}
