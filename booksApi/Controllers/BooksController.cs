using booksApi.Data.Models;
using booksApi.Data.Services;
using booksApi.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBookWithAuthors([FromBody] BookVM bookVM)
        {
            _booksService.AddBookwithAuthors(bookVM);
            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetBooks()
        {
            var bookList = _booksService.GetBooks();
            return Ok(bookList);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _booksService.GetBookById(id);
            if (book == null)
                return Content("No Book Found");
            else
                return Ok(book);
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM bookVM)
        {
            var updatedBook = _booksService.UpdateBookById(id, bookVM);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }

    }
}
