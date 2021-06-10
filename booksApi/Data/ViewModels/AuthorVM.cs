using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }

    public class AuthorWithBooksVm
    {
        public string FullName { get; set; }
        public List<string> ListOfBooks { get; set; }
    }
}
