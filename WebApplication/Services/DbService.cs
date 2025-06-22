using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IDbService
{
   public Task<ICollection<WydarzenieGetDto>> GetWydarzeniaAsync();
   public Task<ICollection<UczestnikGetDto>> GetUczestnikAsync();
   public Task CreateWydarzenieAsync(WydarzenieCreateDto wydarzenieData);
   
   public Task CancelRegistrationAsync(int uczestnikId, int wydarzenieId);

   public Task AddPrelegentsWydarzenieAsync(int wydarzenieId, List<int> prelegentIds);
   
   public Task AddUczestnikWydarzenieAsync(int uczestnikId, int wydarzenieId);
}


public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<WydarzenieGetDto>> GetWydarzeniaAsync()
    {
        return await data.Wydarzenie.Select(w => new WydarzenieGetDto
        {
            Id = w.Id,
            Tytul = w.Tytul,
            Opis = w.Opis,
            Data = w.Data,
            Prelegents = w.PrelegentWydarzenie.Select(pr => new WydarzenieGetDtoPrelegent
            {
                Id = pr.PrelegentId,
                Imie = pr.Prelegent.Imie,
                Nazwisko = pr.Prelegent.Nazwisko
            }).ToList(),
            LiczbaUczestnikow = w.UczestnikWydarzenie.Count,
            LiczbaWolnychMiejsc = w.MaxLiczbaUczestnikow-w.UczestnikWydarzenie.Count
        }).ToListAsync();
    }

    public async Task<ICollection<UczestnikGetDto>> GetUczestnikAsync()
    {
        return await data.Uczestnik.Select(u => new UczestnikGetDto
        {
            Id = u.Id,
            Imie = u.Imie, 
            Nazwisko = u.Nazwisko,
            Wydarzenia = u.UczestnikWydarzenie.Select(w => new UczestnikGetDtoWydarzenie
            {
                Id = w.WydarzenieId,
                Data = w.Wydarzenie.Data,
                Tytul = w.Wydarzenie.Tytul,
                Opis = w.Wydarzenie.Opis,
                Prelegents = w.Wydarzenie.PrelegentWydarzenie.Select(p => new UczestnikGetDtoWydarzeniePrelegent
                {
                    Id = p.PrelegentId,
                    Nazwisko = p.Prelegent.Nazwisko
                }).ToList()
            }).ToList()
            
        }).ToListAsync();
    }

    public async Task CreateWydarzenieAsync(WydarzenieCreateDto wydarzenieData)
    {
        if(DateTime.Compare(DateTime.Now, wydarzenieData.Data)>0)
            throw new BadDateException("Zbyt wczesna data");
        
        var wydarzenie = new Wydarzenie 
        { 
            Tytul = wydarzenieData.Tytul,
            Opis = wydarzenieData.Opis,
            Data = wydarzenieData.Data,
            MaxLiczbaUczestnikow = wydarzenieData.MaxLiczbaUczestnikow
        };
        data.Wydarzenie.Add(wydarzenie);
        await data.SaveChangesAsync();
    }

    public async Task CancelRegistrationAsync(int uczestnikId, int wydarzenieId)
    {
        var reg = await data.UczestnikWydarzenie
            .Include(uw => uw.Wydarzenie)
            .FirstOrDefaultAsync(uw => uw.UczestnikId == uczestnikId && uw.WydarzenieId == wydarzenieId);
        if (reg == null)
            throw new NotFoundException("Nie znaleziono takiej rejestracji");
        if (reg.Wydarzenie.Data.CompareTo(DateTime.Now.AddHours(24))<0)
        {
            throw new BadDateException("Wydarzenie odbędzie się w mniej niż 24 godziny");
        }
        data.UczestnikWydarzenie.Remove(reg);
        await data.SaveChangesAsync();
    }

    public async Task AddPrelegentsWydarzenieAsync(int wydarzenieId, List<int> prelegentIds)
    {
        var wydarzenie = await data.Wydarzenie
            .FirstOrDefaultAsync(w => w.Id == wydarzenieId);
        if (wydarzenie == null)
            throw new NotFoundException($"Nie znaleziono wydarzenia o id: {wydarzenieId}");
        foreach (int id in prelegentIds)
        {
            var prelegent = await data.Prelegent
                .FirstOrDefaultAsync(p => p.Id == id);
            if (prelegent == null)
            {
                throw new NotFoundException($"Nie znaleziono prelegenta o id: {id}");
            }

            var wydarzeniaPrelegenta = prelegent.PrelegentWydarzenie.ToList();
            foreach (var w in wydarzeniaPrelegenta)
            {
                if (w.Wydarzenie.Data == wydarzenie.Data)
                {
                    throw new BadDateException("Prelegent nie może mieć 2 wydarzeń o tej samej dacie");
                }
            }

            var prelegentWydarzenie = new PrelegentWydarzenie
            {
                WydarzenieId = wydarzenieId,
                PrelegentId = prelegent.Id
            };
            data.PrelegentWydarzenie.Add(prelegentWydarzenie);
        }
        await data.SaveChangesAsync();
    }

    public async Task AddUczestnikWydarzenieAsync(int uczestnikId, int wydarzenieId)
    {
        var wydarzenie = await data.Wydarzenie
            .FirstOrDefaultAsync(w => w.Id == wydarzenieId);
        if (wydarzenie == null)
        {
            throw new NotFoundException("Nie znalezniono wydarzenia");
        }
        int liczbaWolnychMiejsc = wydarzenie.MaxLiczbaUczestnikow - wydarzenie.UczestnikWydarzenie.Count;
        if (liczbaWolnychMiejsc < 1)
        {
            throw new FullEventException("Brak dostępnych miejsc na wydarzenie");
        }
        var uczestnik = data.Uczestnik
            .FirstOrDefaultAsync(u => u.Id == uczestnikId);
        if (uczestnik == null)
        {
            throw new NotFoundException("Nie znaleziono uczestnika");
        }
        
        var listaUczestnikWydarzenie = data
            .UczestnikWydarzenie
            .FirstOrDefaultAsync(uw => uw.Uczestnik.Id == uczestnikId && uw.WydarzenieId == wydarzenieId);
        if (listaUczestnikWydarzenie != null)
        {
            throw new AlreadyRegisteredException("Uczestnik już jest zarejestrowany na wydarzenie");
        }

        var uczestnikWydarzenie = new UczestnikWydarzenie
        {
            UczestnikId = uczestnikId,
            WydarzenieId = wydarzenieId
        };
        data.UczestnikWydarzenie.Add(uczestnikWydarzenie);
        await data.SaveChangesAsync();
    }
}