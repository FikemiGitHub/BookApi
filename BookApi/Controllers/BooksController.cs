using BookApi.Models;
using BookApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // Using ActionResult
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        //{
        //    var result = await _bookRepository.GetAllBooks();
        //    return Ok(result);
        //}

        // Using IActionResult
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookRepository.GetAllBooks();
            return Ok(result);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            return Ok(book);
        }

        [HttpPost("CreateNewBook")]
        public async Task<IActionResult> CreateBook([FromBody]Book book)
        {
            var newBook = await _bookRepository.CreateBook(book);
            return CreatedAtAction(nameof(GetBookById), new {id = newBook.Id}, newBook);
        }

        [HttpPut("UpdateBookById/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody]Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Header Id does not jive with Book Id");
            }
            var newBook = await _bookRepository.UpdateBook(book);
            return Ok(newBook);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookToDelete = await _bookRepository.GetBookById(id);

            if (bookToDelete == null)
            {
                return NotFound("Book was not found");
            }

            await _bookRepository.DeleteBook(id);

            return Ok("Book deleted successfully");
        }
    }
}
