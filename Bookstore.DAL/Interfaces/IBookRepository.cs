using Bookstore.Domain.Entities;

namespace Bookstore.DAL.Interfaces;

public interface IBookRepository
{
    public Task<Book> GetById(int id);

    public Task<List<Book>> GetByFilter(string? title, DateTime? date);
}
