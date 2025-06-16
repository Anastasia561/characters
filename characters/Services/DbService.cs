using characters.Data;
using characters.Dtos;
using characters.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace characters.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CharacterDto> GetCharacterAsync(int characterId, CancellationToken cancellationToken)
    {
        var character = await _context.Characters.Where(c => c.Id == characterId)
            .Select(c => new CharacterDto()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight = c.MaxWeight,
                BackpackItems = c.Backpacks.Select(b => new BackpackItemDto()
                {
                    ItemName = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount = b.Amount
                }).ToList(),
                Titles = c.CharacterTitles.Select(t => new TitleDto()
                {
                    Title = t.Title.Name,
                    AcquiredAt = t.AcquiredAt
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);

        if (character == null)
            throw new NotFoundException($"Character with id {characterId} not found");

        return character;
    }
}