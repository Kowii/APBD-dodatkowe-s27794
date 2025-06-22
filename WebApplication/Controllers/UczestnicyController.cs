using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UczestnicyController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUczestnicy()
    {
        return Ok(await dbService.GetUczestnikAsync());
    }

    [HttpDelete("{uczestnikId}/{wydarzenieId}")]
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
    
}