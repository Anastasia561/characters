using characters.Dtos;

namespace characters.Services;

public interface IDbService
{
    public Task<CharacterDto> GetCharacterAsync(int characterId, CancellationToken cancellationToken);
}