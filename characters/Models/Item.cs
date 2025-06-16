using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace characters.Models;

[Table("Item")]
public class Item
{
    [Key] public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; }
    public int Weight { get; set; }
    public ICollection<Backpack> Backpacks { get; set; } = null!;
}