using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Word;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordsController(IWordService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(WordCreateDTO dto)
    {
        await _service.CreateAsync(dto);
        return Created();
    }

    [HttpPost("AddRange")]
    public async Task<IActionResult> Range(IEnumerable<WordCreateDTO> dtos)
    {

        foreach (WordCreateDTO dto in dtos)
        {
            await _service.CreateAsync(dto);
        }
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAsync());
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, WordUpdateDTO dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
