using characters.Data;
using characters.Dtos;
using characters.Exceptions;
using characters.Models;
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

    public async Task AddCharacterItemsAsync(List<int> itemIds, int characterId, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId, cancellationToken);
            if (character == null)
                throw new NotFoundException($"Character with id {characterId} not found");

            foreach (var id in itemIds)
            {
                var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
                if (item == null)
                    throw new NotFoundException($"Item with id {id} not found");

                if (character.MaxWeight < character.CurrentWeight + item.Weight)
                    throw new ConflictException(
                        $"Character with id {characterId} does not have enough weight to add the item");

                var backpack =
                    await _context.Backpacks.FirstOrDefaultAsync(b => b.CharacterId == characterId && b.ItemId == id,
                        cancellationToken);
                if (backpack == null)
                {
                    _context.Backpacks.Add(new Backpack()
                    {
                        CharacterId = characterId,
                        ItemId = id,
                        Amount = 1
                    });
                }
                else
                {
                    backpack.Amount = backpack.Amount + 1;
                }

                character.CurrentWeight += item.Weight;
                await _context.SaveChangesAsync(cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}