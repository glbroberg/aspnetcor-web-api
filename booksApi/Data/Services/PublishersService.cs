using booksApi.Data.Models;
using booksApi.Data.Paging;
using booksApi.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Data.Services
{
    public class PublishersService
    {
        AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisherVM)
        {
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var pubList = _context.Publishers.OrderBy(p => p.Name).ToList();

            // *** Sort
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        pubList = pubList.OrderByDescending(p => p.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            // *** Search
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                pubList = pubList.Where(x => x.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // *** Paging
            int pageSize = 5;
            pubList = PaginatedList<Publisher>.Create(pubList.AsQueryable(), pageNumber ?? 1, pageSize);

            return pubList;
        }

        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherID)
        {
            var _publisherData = _context.Publishers.Where(p => p.Id == publisherID).Select(pub => new PublisherWithBooksAndAuthorsVM
            {
                Name = pub.Name,
                BookAuthors = pub.Books.Select(b => new BookAuthorVM
                {
                    BookName = b.Title,
                    Authors = b.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int publisherId)
        {
            var publisherToDelete = _context.Publishers.FirstOrDefault(p => p.Id == publisherId);
            if (publisherToDelete != null)
            {
                _context.Publishers.Remove(publisherToDelete);
                _context.SaveChanges();
            }
        }
    }
}
