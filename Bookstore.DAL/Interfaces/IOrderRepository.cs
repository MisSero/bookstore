using Bookstore.DAL.Entities;

namespace Bookstore.DAL.Interfaces;

public interface IOrderRepository
{
    public Task<bool> Save(int[] bookIds);

    public Task<List<Order>> GetByFilter(int id, DateTime? date);
}
