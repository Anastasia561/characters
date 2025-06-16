using characters.Dtos;

namespace characters.Services;

public interface IDbService
{
    public Task<CharacterDto> GetCharacterAsync(int characterId, CancellationToken cancellationToken);
    public Task AddCharacterItemsAsync(List<int> itemIds, int characterId, CancellationToken cancellationToken);
}