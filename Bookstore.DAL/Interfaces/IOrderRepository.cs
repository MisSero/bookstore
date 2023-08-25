using Bookstore.DAL.Entities;

namespace Bookstore.DAL.Interfaces;

public interface IOrderRepository
{
    public Task<bool> Save();

    public Task<List<Order>> GetByFilter(int number, DateTime? date);
}
