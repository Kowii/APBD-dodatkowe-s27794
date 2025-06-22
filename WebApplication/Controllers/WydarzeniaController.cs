using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class WydarzeniaController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetWydarzeniaAsync()
    {
        return Ok(await dbService.GetWydarzeniaAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddWydarzenie([FromBody] WydarzenieCreateDto wydarzenieData)
    {
        try
        {
            await dbService.CreateWydarzenieAsync(wydarzenieData);
            return Ok();
        }
        catch (BadDateException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{wydarzenieId}/{uczestnikId}")]
    public async Task<IActionResult> CancelRegistrationAsync([FromRoute] int uczestnikId, int wydarzenieId)
    {
        try
        {
            await dbService.CancelRegistrationAsync(uczestnikId, wydarzenieId);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadDateException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{wydarzenieId}")]
    public async Task<IActionResult> AddPrelegentsWydarzenieAsync(
        [FromRoute] int wydarzenieId, [FromBody] List<int> prelegentIds)
    {
        try
        {
            await dbService.AddPrelegentsWydarzenieAsync(wydarzenieId, prelegentIds);
            return Ok();
        }
        catch (BadDateException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPut("{wydarzenieId}/{uczestnikId}")]
    public async Task<IActionResult> AddUczestnikWydarzenieAsync([FromRoute] int uczestnikId, int wydarzenieId)
    {
        try
        {
            await dbService.AddUczestnikWydarzenieAsync(uczestnikId, wydarzenieId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (FullEventException e)
        {
            return BadRequest(e.Message);
        }
        catch (AlreadyRegisteredException e)
        {
            return BadRequest(e.Message);
        }
    }

}