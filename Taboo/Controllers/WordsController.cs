using Microsoft.AspNetCore.Mvc;
using Taboo.DTOs.Word;
using Taboo.Exceptions;
using Taboo.Services.Abstracts;

namespace Taboo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordsController(IWordService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(WordCreateDTO dto)
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

    [HttpPost("AddRange")]
    public async Task<IActionResult> Range(IEnumerable<WordCreateDTO> dtos)
    {
        try
        {
            foreach (WordCreateDTO dto in dtos)
            {
                await _service.CreateAsync(dto);
            }
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

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, WordUpdateDTO dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
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
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
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
