using Bookstore.DAL.Entities;
using Bookstore.Domain.Models;

namespace Bookstore.Domain.Mappers;

public static class OrderMapper
{
    public static OrderModel EntityToModel(Order order)
    {
        List<BookModel> books = new ();
        foreach (Book book in order.Books)
        {
            books.Add(BookMapper.EntityToModel(book));
        }

        OrderModel orderModel = new ()
        {
            Number = order.Id,
            Created = order.Created,
            Cost = order.Cost,
            Books = books
        };

        return orderModel;
    }
}
