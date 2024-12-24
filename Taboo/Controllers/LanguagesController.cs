using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Language;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController(ILanguageService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(LanguageCreateDTO dto)
    {
        await _service.CreateAsync(dto);
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAsync());
    }

    [HttpGet("GetByCode")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(await _service.GetByCodeAsync(code));
    }

    [HttpPut]
    public async Task<IActionResult> Update(string code, LanguageUpdateDTO dto)
    {
        await _service.UpdateAsync(code, dto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string code)
    {
        await _service.DeleteAsync(code);
        return Ok();
    }
}
