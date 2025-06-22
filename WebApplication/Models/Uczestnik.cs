using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
[Table("Uczestnik")]
public class Uczestnik
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [MaxLength(50)] public string Imie { get; set; } = null!;
    [MaxLength(50)] public string Nazwisko { get; set; } = null!;
    
    public virtual ICollection<UczestnikWydarzenie> UczestnikWydarzenie { get; set; } = null!; 
}