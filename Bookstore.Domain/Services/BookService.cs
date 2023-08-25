using System.Net;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interfaces;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Responces;

namespace Bookstore.Domain.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ServiceResponse<List<Book>>> GetByFilter(string? title, DateTime? date)
    {
        ServiceResponse<List<Book>> response = new();

        var filteredBooks = _bookRepository.GetByFilter(title, date).Result;

        if (filteredBooks == null)
            response.StatusCode = HttpStatusCode.NotFound;
        else
        {
            response.Data = filteredBooks;
            response.StatusCode = HttpStatusCode.OK;
        }

        return response;
    }

    public async Task<ServiceResponse<Book>> GetById(int id)
    {
        ServiceResponse<Book> response = new();

        Book book = await _bookRepository.GetById(id);

        if (book == null)
            response.StatusCode = HttpStatusCode.NotFound;
        else
        {
            response.Data = book;
            response.StatusCode = HttpStatusCode.OK;
        }

        return response;
    }
}
