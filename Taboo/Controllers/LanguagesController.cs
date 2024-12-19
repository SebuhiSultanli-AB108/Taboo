using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Language;
using Taboo.Exceptions;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController(ILanguageService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(LanguageCreateDTO dto)
    {
        try
        {
            await _service.CreateAsync(dto);
            return Created();
        }
        catch (Exception ex)
        {
            if (ex is IBaseException ibe)
                return StatusCode(ibe.StatusCode, new
                {
                    StatusCode = ibe.StatusCode,
                    Message = ibe.ErrorMessage
                });
            else
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
            throw;
        }
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
        try
        {
            await _service.UpdateAsync(code, dto);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex is IBaseException ibe)
                return StatusCode(ibe.StatusCode, new
                {
                    StatusCode = ibe.StatusCode,
                    Message = ibe.ErrorMessage
                });
            else
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
            throw;
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string code)
    {
        try
        {
            await _service.DeleteAsync(code);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex is IBaseException ibe)
                return StatusCode(ibe.StatusCode, new
                {
                    StatusCode = ibe.StatusCode,
                    Message = ibe.ErrorMessage
                });
            else
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
            throw;
        }
    }
}
