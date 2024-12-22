using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Game;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IGameService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(GameCreateDTO dto)
    {
        return Ok(await _service.AddAsync(dto));
    }

    [HttpPost("Start")]
    public async Task<IActionResult> Start(Guid id)
    {
        await _service.StartAsync(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetCurrentStatus(id));
    }
}
