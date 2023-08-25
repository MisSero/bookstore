using System.Net;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interfaces;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Mappers;
using Bookstore.Domain.Models;
using Bookstore.Domain.Responces;

namespace Bookstore.Domain.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ServiceResponse<List<BookModel>>> GetByFilter(string? title, DateTime? date)
    {
        ServiceResponse<List<BookModel>> response = new();

        var filteredBooks = _bookRepository.GetByFilter(title, date).Result;

        if (filteredBooks == null)
            response.StatusCode = HttpStatusCode.NotFound;
        else
        {
            List<BookModel> books = new ();

            foreach (var book in filteredBooks)
            {
                books.Add(BookMapper.EntityToModel(book));
            }

            response.Data = books;
            response.StatusCode = HttpStatusCode.OK;
        }

        return response;
    }

    public async Task<ServiceResponse<BookModel>> GetById(int id)
    {
        ServiceResponse<BookModel> response = new();

        Book book = await _bookRepository.GetById(id);

        if (book == null)
            response.StatusCode = HttpStatusCode.NotFound;
        else
        {
            response.Data = BookMapper.EntityToModel(book);
            response.StatusCode = HttpStatusCode.OK;
        }

        return response;
    }
}
