using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Prelegent> Prelegent { get; set; }
    public DbSet<Wydarzenie> Wydarzenie { get; set; }
    public DbSet<Uczestnik> Uczestnik { get; set; }
    public DbSet<UczestnikWydarzenie> UczestnikWydarzenie { get; set; }
    public DbSet<PrelegentWydarzenie> PrelegentWydarzenie { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var prelegents = new List<Prelegent>
        {
            new()
            {
                Id = 1,
                Imie = "Ferdynand",
                Nazwisko = "Kiepski"
            },
            new()
            {
                Id = 2,
                Imie = "Marian",
                Nazwisko = "Paździoch"
            },
            new()
            {
                Id = 3,
                Imie = "Arnold",
                Nazwisko = "Boczek"
            }
        };

        var uczestniks = new List<Uczestnik>
        {
            new()
            {
                Id = 1,
                Imie = "Patryk",
                Nazwisko = "Bejtman"
            },
            new()
            {
                Id = 2,
                Imie = "Anatoli",
                Nazwisko = "Niebochód",
            },
            new()
            {
                Id = 3,
                Imie = "Yoshikage",
                Nazwisko = "Kira"
            },
            new()
            {
                Id = 4,
                Imie = "Janusz",
                Nazwisko = "Tracz"
            },
            new()
            {
                Id = 5,
                Imie = "Porucznik",
                Nazwisko = "Kolombo"
            }
        };

        var wydarzenies = new List<Wydarzenie>
        {
            new()
            {
                Id = 1,
                MaxLiczbaUczestnikow = 50,
                Tytul = "Otwarcie parasola",
                Opis = "Wielkie otwarcie parasola w Warszawie",
                Data = new DateTime(2025,09,09)
            },
            new()
            {
                Id = 2,
                MaxLiczbaUczestnikow = 2,
                Tytul = "Kurs parowania",
                Opis = "Mikrokurs łączenia w pary w Krakowie",
                Data = new DateTime(2025,07,09)
            }
        };
        var prelegentWydarzenie = new List<PrelegentWydarzenie>
        {
            new()
            {
                PrelegentId = 1,
                WydarzenieId = 1,
            },
            new()
            {
                PrelegentId = 2,
                WydarzenieId = 2,
            }
        };

        var uczestnikWydarzenie = new List<UczestnikWydarzenie>
        {
            new()
            {
                UczestnikId = 1,
                WydarzenieId = 1
            },
            new()
            {
                UczestnikId = 2,
                WydarzenieId = 2
            },
            new()
            {
                UczestnikId = 3,
                WydarzenieId = 1
            }
        };

        modelBuilder.Entity<PrelegentWydarzenie>().HasData(prelegentWydarzenie);
        modelBuilder.Entity<UczestnikWydarzenie>().HasData(uczestnikWydarzenie);
        modelBuilder.Entity<Wydarzenie>().HasData(wydarzenies);
        modelBuilder.Entity<Uczestnik>().HasData(uczestniks);
        modelBuilder.Entity<Prelegent>().HasData(prelegents);
    }

}