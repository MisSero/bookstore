using Bookstore.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        public BooksController(IBookRepository bookRepository)
        {
            _repository = bookRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = _repository.GetById(id).Result;

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetByFilter(string? title, DateTime date)
        {
            var books = _repository.GetByFilter(title, date).Result;

            if (books  == null) 
                return NotFound();
            return Ok(books);
        }
    }
}
