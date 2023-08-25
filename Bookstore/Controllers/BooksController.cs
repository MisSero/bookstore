using Microsoft.AspNetCore.Mvc;
using Bookstore.DAL.Interfaces;
using Bookstore.Domain.Interfaces;
using System.Net;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _bookService.GetById(id);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response.Data);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetByFilter(string? title, DateTime? date)
        {
            var response = await _bookService.GetByFilter(title, date);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response.Data);
            return NotFound();
        }
    }
}
