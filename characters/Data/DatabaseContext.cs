using characters.Models;
using Microsoft.EntityFrameworkCore;

namespace characters.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new Character() { Id = 1, FirstName = "John", LastName = "Doe", CurrentWeight = 10, MaxWeight = 30 },
            new Character() { Id = 2, FirstName = "Ann", LastName = "Doe", CurrentWeight = 13, MaxWeight = 40 }
        });

        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item { Id = 1, Name = "item1", Weight = 5 },
            new Item { Id = 2, Name = "item2", Weight = 10 }
        });

        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new Title { Id = 1, Name = "title1" },
            new Title { Id = 2, Name = "title2" }
        });

        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new Backpack() { ItemId = 1, CharacterId = 1, Amount = 2 },
            new Backpack() { ItemId = 2, CharacterId = 1, Amount = 1 }
        });

        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>()
        {
            new CharacterTitle() { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2020-01-01") },
            new CharacterTitle() { CharacterId = 1, TitleId = 2, AcquiredAt = DateTime.Parse("2022-01-01") }
        });
    }
}