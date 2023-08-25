using Bookstore.DAL.Entities;
using Bookstore.Domain.Models;

namespace Bookstore.Domain.Mappers;
public static class BookMapper
{
    public static BookModel EntityToModel(Book book)
    {
        BookModel bookModel = new BookModel()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Released = book.Released.ToShortDateString(),
            Price = book.Price,
        };

        return bookModel;
    }
}