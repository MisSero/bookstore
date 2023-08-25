using Bookstore.DAL.Interfaces;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _db;

    public BookRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Book>> GetByFilter(string? title, DateTime? date)
    {
        var filteredBooks = _db.Books.AsQueryable();

        if (!string.IsNullOrEmpty(title))
            filteredBooks = filteredBooks.Where(b => b.Title.Contains(title));

        if (date.HasValue)
            filteredBooks = filteredBooks.Where(b => b.Released.Date == date.Value.Date);

        return await filteredBooks.ToListAsync();
    }

    public async Task<Book> GetById(int id)
    {
        return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
    }
}
