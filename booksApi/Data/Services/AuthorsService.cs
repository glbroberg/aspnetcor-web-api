using booksApi.Data.Models;
using booksApi.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Data.Services
{
    public class AuthorsService
    {
        AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var _author = new Author()
            {
                FullName = authorVM.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVm GetAuthorWithBooks(int authorId)
        {
            var authorWithBooks = _context.Authors.Where(x => x.Id == authorId).Select(author => new AuthorWithBooksVm
            {
                FullName = author.FullName,
                ListOfBooks = author.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return authorWithBooks;
        }
    }
}
