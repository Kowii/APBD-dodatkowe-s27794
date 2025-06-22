using WebApplication1.Models;

namespace WebApplication1.DTOs;

public class WydarzenieGetDto
{
    public int Id { get; set; }
    public string Tytul { get; set; } = null!;
    public string Opis { get; set; } = null!;
    public DateTime Data { get; set; }
    public ICollection<WydarzenieGetDtoPrelegent> Prelegents { get; set; } = null!;
    public int LiczbaUczestnikow { get; set; }
    public int LiczbaWolnychMiejsc { get; set; }
}

public class WydarzenieGetDtoPrelegent
{
    public int Id { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
}