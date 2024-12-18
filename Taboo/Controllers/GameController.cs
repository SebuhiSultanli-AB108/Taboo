using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Language;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController(ILanguageService _service) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(LanguageDTO dto)
    {
        await _service.CreateAsync(dto);

        return Created();
    }
}
