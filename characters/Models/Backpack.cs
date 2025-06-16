using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace characters.Models;

[Table("Backpack")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class Backpack
{
    [ForeignKey(nameof(Character))] public int CharacterId { get; set; }
    public Character Character { get; set; }
    [ForeignKey(nameof(Item))] public int ItemId { get; set; }
    public Item Item { get; set; }
    public int Amount { get; set; }
}