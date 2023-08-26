using Bookstore.Domain.Models;
using Bookstore.Domain.Responces;

namespace Bookstore.Domain.Interfaces;

public interface IOrderService
{
    public Task<ServiceResponse<List<OrderModel>>> GetByFilter(int number, DateTime? date);

    public Task<ServiceResponse<bool>> Save(int[] Ids);
}
