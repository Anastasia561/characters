using characters.Data;
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
}