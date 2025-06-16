using characters.Exceptions;
using characters.Services;
using Microsoft.AspNetCore.Mvc;

namespace characters.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterAsync(int characterId, CancellationToken cancellationToken)
    {
        try
        {
            var character = await _dbService.GetCharacterAsync(characterId, cancellationToken);
            return Ok(character);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddCharacterItemsAsync(int characterId, List<int> itemIds,
        CancellationToken cancellationToken)
    {
        try
        {
            await _dbService.AddCharacterItemsAsync(itemIds, characterId, cancellationToken);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
}