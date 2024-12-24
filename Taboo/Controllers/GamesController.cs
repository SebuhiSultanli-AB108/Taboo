using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Game;
using Taboo.ExternalServices.Abstracts;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IGameService _service, ICacheService _cache) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(GameCreateDTO dto)
    {
        return Ok(await _service.AddAsync(dto));
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Start(Guid id)
    {
        return Ok(await _service.StartAsync(id));
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Success(Guid id)
    {

        return Ok(await _service.SuccessAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetCurrentStatus(id));
    }
}
