using Bookstore.Domain.Models;
using Bookstore.Domain.Responces;

namespace Bookstore.Domain.Interfaces;

public interface IBookService
{
    public Task<ServiceResponse<BookModel>> GetById(int id);

    public Task<ServiceResponse<List<BookModel>>> GetByFilter(string? title, DateTime? date);
}
