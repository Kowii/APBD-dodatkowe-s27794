using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class WydarzenieCreateDto
{
    [MaxLength(50)] [Required] public string Tytul { get; set; } = null!;
    [MaxLength(250)] [Required] public string Opis { get; set; } = null!;
    [Required] public DateTime Data { get; set; }
    [Required][Range(1,9999999)] public int MaxLiczbaUczestnikow { get; set; }
}