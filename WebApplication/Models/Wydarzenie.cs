using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
[Table("Wydarzenie")]
public class Wydarzenie
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [MaxLength(50)] public string Tytul { get; set; } = null!;
    [MaxLength(50)] public string Opis { get; set; } = null!;
    public DateTime Data {get; set; }
    [Column("max_liczba_uczestnikow")]
    public int MaxLiczbaUczestnikow { get; set; }
    
    public virtual ICollection<UczestnikWydarzenie> UczestnikWydarzenie { get; set; } = null!; 
    public virtual ICollection<PrelegentWydarzenie> PrelegentWydarzenie { get; set; } = null!; 
}