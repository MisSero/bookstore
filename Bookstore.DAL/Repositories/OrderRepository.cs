using Bookstore.DAL.Entities;
using Bookstore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Bookstore.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;

    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }

    /* First get orders together with BookOrder and Book, 
     * then for each order select Books from BookOrder 
     * to get the multiple copies of the same books */
    public async Task<List<Order>> GetByFilter(int id, DateTime? date)
    {
        var filteredOrders = _db.Orders.AsQueryable();

        if (id != 0)
            filteredOrders = filteredOrders.Where(o => o.Id == id);

        if (date.HasValue)
            filteredOrders = filteredOrders.Where(b => b.Created.Date == date.Value.Date);

        var orders = await filteredOrders
            .Include(o => o.BookOrders)
            .ThenInclude(bo => bo.Book)
            .ToListAsync();

        foreach (var order in orders)
        {
            order.Books = order.BookOrders.Select(bo => bo.Book).ToList();
        }

        
        return orders;
    }

    /* Use transaction to add order and get its Id to create BookOrder instances 
     * to be able to add multiple copies of the same book to order */
    public async Task<bool> Save(int[] bookIds)
    {
        using (var transactionScope = new TransactionScope(
            TransactionScopeAsyncFlowOption.Enabled))
        {
            decimal cost = 0;
            List<BookOrder> booksOrder = new();
            Order order = new()
            {
                Created = DateTime.Now,
                Cost = cost,
            };

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            int orderId = order.Id;

            foreach (var bookId in bookIds)
            {
                var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

                if (book == null)
                    return false;
                booksOrder.Add(new BookOrder()
                {
                    BookId = bookId,
                    OrderId = orderId
                });
                cost += book.Price;
            }

            order.Cost = cost;
            order.Created = DateTime.Now;

            _db.Orders.Update(order);
            await _db.AddRangeAsync(booksOrder);
            await _db.SaveChangesAsync();

            transactionScope.Complete();

            return true;
        }
    }
}
