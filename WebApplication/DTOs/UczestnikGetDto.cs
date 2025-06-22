namespace WebApplication1.DTOs;

public class UczestnikGetDto
{
    public int Id { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public ICollection<UczestnikGetDtoWydarzenie> Wydarzenia { get; set; } = null!;
}

public class UczestnikGetDtoWydarzenie
{
    public int Id { get; set; }
    public string Tytul { get; set; } = null!;
    public string Opis { get; set; } = null!;
    public DateTime Data { get; set; }
    public ICollection<UczestnikGetDtoWydarzeniePrelegent> Prelegents { get; set; } = null!;
}

public class UczestnikGetDtoWydarzeniePrelegent
{
    public int Id { get; set; }
    public string Nazwisko { get; set; } = null!;
}