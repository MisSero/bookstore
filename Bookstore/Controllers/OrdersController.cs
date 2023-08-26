using System.Net;
using Bookstore.DAL.Interfaces;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Mappers;
using Bookstore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] int[] bookIds)
        {
            if ((await _orderService.Save(bookIds)).StatusCode == HttpStatusCode.OK)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetByFilter(int number, DateTime? date)
        {
            var response = await _orderService.GetByFilter(number, date);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response.Data);
            return StatusCode(500, "Internal server error");
        }
    }
}
