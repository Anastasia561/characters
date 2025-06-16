using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace characters.Models;

[Table("Title")]
public class Title
{
    [Key] public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; }
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = null!;
}