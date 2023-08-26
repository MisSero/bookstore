using Bookstore.DAL.Interfaces;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Mappers;
using Bookstore.Domain.Models;
using Bookstore.Domain.Responces;
using System.Net;

namespace Bookstore.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ServiceResponse<List<OrderModel>>> GetByFilter(int number, DateTime? date)
    {
        ServiceResponse<List<OrderModel>> response = new();

        var filteredOrders = await _orderRepository.GetByFilter(number, date);

        if (filteredOrders == null)
            response.StatusCode = HttpStatusCode.InternalServerError;
        else
        {
            List<OrderModel> orders = new();

            foreach (var order in filteredOrders)
                orders.Add(OrderMapper.EntityToModel(order));

            response.Data = orders;
            response.StatusCode = HttpStatusCode.OK;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> Save(int[] Ids)
    {
        ServiceResponse<bool> response = new();

        if (await _orderRepository.Save(Ids))
        {
            response.Data = true;
            response.StatusCode = HttpStatusCode.OK;
        }
        else
            response.StatusCode = HttpStatusCode.BadRequest;

        return response;
    }
}
