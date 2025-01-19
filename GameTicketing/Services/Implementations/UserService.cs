using Microsoft.EntityFrameworkCore;
using GameTicketing.Database;
using GameTicketing.Database.Models;
using GameTicketing.Services.Abstractions;
using GameTicketing.DataTransferObjects;

namespace GameTicketing.Services.Implementations;

public class UserService : IUserService
{
    private readonly TicketingDatabaseContext _databaseContext;

    public UserService(TicketingDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddUser(UsersAddRecord user)
    {
        if (user == null) 
            throw new ArgumentNullException(nameof(user));

        await _databaseContext.Set<Users>().AddAsync(new Users
        {
            Nume = user.Nume,
            Prenume = user.Prenume,
            Functie = user.Functie,
            Telefon = user.Telefon,
            Email = user.Email
        });
        await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateUser(UsersUpdateRecord user)
    {
        if (user == null) 
            throw new ArgumentNullException(nameof(user));

        var entry = await _databaseContext.Set<Users>()
            .FirstOrDefaultAsync(e => e.Id == user.Id);

        if (entry == null)
            throw new Exception("User not found.");

        entry.Nume = user.Nume;
        entry.Prenume = user.Prenume;
        entry.Functie = user.Functie;
        entry.Telefon = user.Telefon;
        entry.Email = user.Email;

        _databaseContext.Set<Users>().Update(entry);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int userId)
    {
        var user = await _databaseContext.Set<Users>()
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (user == null)
            throw new Exception("User not found.");

        _databaseContext.Set<Users>().Remove(user);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<UsersRecord> GetUser(int userId)
    {
        var user = await _databaseContext.Set<Users>()
            .Where(e => e.Id == userId)
            .Select(e => new UsersRecord
            {
                id = e.Id,
                Nume = e.Nume,
                Prenume = e.Prenume,
                Functie = e.Functie,
                Telefon = e.Telefon,
                Email = e.Email
            })
            .FirstOrDefaultAsync();

        if (user == null)
            throw new Exception("User not found.");

        return user;
    }

    public async Task<List<UsersRecord>> GetUsers()
    {
        return await _databaseContext.Set<Users>()
            .Select(e => new UsersRecord
            {
                id = e.Id,
                Nume = e.Nume,
                Prenume = e.Prenume,
                Functie = e.Functie,
                Telefon = e.Telefon,
                Email = e.Email
            })
            .ToListAsync();
    }
    
    public class PaginationResponse<T>
    {
        public int Page { get; set; }          // Pagina curentă
        public int PageSize { get; set; }      // Numărul de elemente per pagină
        public int TotalRecords { get; set; }  // Totalul înregistrărilor în baza de date
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize); // Totalul paginilor
        public List<T> Data { get; set; }      // Datele returnate pentru pagina curentă
    }


    public async Task<PaginationResponse<UsersRecord>> GetPagedUsers(PaginationQueryParams query)
    {
        if (query.Page <= 0 || query.PageSize <= 0)
            throw new ArgumentException("Invalid pagination parameters.");

        var totalRecords = await _databaseContext.Set<Users>().CountAsync();
        var data = await _databaseContext.Set<Users>()
            .OrderBy(e => e.Id)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(e => new UsersRecord
            {
                id = e.Id,
                Nume = e.Nume,
                Prenume = e.Prenume,
                Functie = e.Functie,
                Telefon = e.Telefon,
                Email = e.Email
            })
            .ToListAsync();
        

        return new PaginationResponse<UsersRecord>
        {
            Page = query.Page,
            PageSize = query.PageSize,
            TotalRecords = totalRecords,
            Data = data
        };
    }
}
