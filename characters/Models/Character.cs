﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace characters.Models;

[Table("Character")]
public class Character
{
    [Key] public int Id { get; set; }
    [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(120)] public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<Backpack> Backpacks { get; set; } = null!;
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = null!;
}