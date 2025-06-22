using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;
[Table("Uczestnik_Wydarzenie")]
[PrimaryKey(nameof(WydarzenieId), nameof(UczestnikId))]
public class UczestnikWydarzenie
{
    [Column("Uczestnik_ID")] 
    public int UczestnikId { get; set; } 
    
    [Column("Wydarzenie_ID")]
    public int WydarzenieId { get; set; }
    

    [ForeignKey(nameof(UczestnikId))]
    public virtual Uczestnik Uczestnik { get; set; } = null!; 
    
    [ForeignKey(nameof(WydarzenieId))]
    public virtual Wydarzenie Wydarzenie { get; set; } = null!; 
    
}