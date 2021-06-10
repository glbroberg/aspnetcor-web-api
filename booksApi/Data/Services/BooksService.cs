using booksApi.Data.Models;
using booksApi.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookwithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                //  *** No longer needed now that AuthorId has been added below
                //Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Book_Authors.Add(_book_author);
            }

            _context.SaveChanges();
        }

        public List<Book> GetBooks() => _context.Books.ToList();

        public BookWithAuthorsVM GetBookById(int id)
        {
            var bookWithAuthors = _context.Books.Where(n => n.Id == id).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,

                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return bookWithAuthors;
        }

        public Book UpdateBookById(int id, BookVM bookVM)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.IsRead = bookVM.IsRead;
                book.DateRead = bookVM.IsRead ? book.DateRead : null;
                book.Rate = bookVM.IsRead ? book.Rate : null;
                book.Genre = bookVM.Genre;
                //book.Author = bookVM.Author;
                book.CoverUrl = bookVM.CoverUrl;

                _context.SaveChanges();
            }

            return book;
        }

        public void DeleteBookById(int id)
        {
            var bookToDelete = _context.Books.FirstOrDefault(b => b.Id == id);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }
        }
    }
}
