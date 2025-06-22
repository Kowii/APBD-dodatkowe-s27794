using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;
[Table("Prelegent_Wydarzenie")]
[PrimaryKey(nameof(WydarzenieId), nameof(PrelegentId))]
public class PrelegentWydarzenie
{
    [Column("Prelegent_ID")] 
    public int PrelegentId { get; set; } 
    
    [Column("Wydarzenie_ID")]
    public int WydarzenieId { get; set; }

    [ForeignKey(nameof(PrelegentId))]
    public virtual Prelegent Prelegent { get; set; } = null!; 
    
    [ForeignKey(nameof(WydarzenieId))]
    public virtual Wydarzenie Wydarzenie { get; set; } = null!; 
    
}