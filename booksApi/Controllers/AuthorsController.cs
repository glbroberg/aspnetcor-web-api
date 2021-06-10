using booksApi.Data.Services;
using booksApi.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        AuthorsService _authorsService; 

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM authorVM)
        {
            _authorsService.AddAuthor(authorVM);
            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{authorId}")]
        public IActionResult GetAuthorWithBooksById(int authorId)
        {
            var author = _authorsService.GetAuthorWithBooks(authorId);
            return Ok(author);
        }
    }
}
