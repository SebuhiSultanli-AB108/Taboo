using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Language;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController(ILanguageService _service) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Post(LanguageDTO dto)
    {
        await _service.CreateAsync(dto);
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string code)
    {
        var result = await _service.DeleteAsync(code);
        if (!result) return NotFound();
        return Ok();
    }


    [HttpPut]
    public async Task<IActionResult> Update(string code, LanguageDTO dto)
    {
        var result = await _service.UpdateAsync(code, dto);
        if (!result) return NotFound();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAsync();
        return Ok(data);
    }

}
