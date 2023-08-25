using Bookstore.DAL.Entities;
using Bookstore.Domain.Responces;

namespace Bookstore.Domain.Interfaces;

public interface IBookService
{
    public Task<ServiceResponse<Book>> GetById(int id);

    public Task<ServiceResponse<List<Book>>> GetByFilter(string? title, DateTime? date);
}
