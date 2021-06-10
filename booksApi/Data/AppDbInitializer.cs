using booksApi.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            // ***ASP.NET Core application service provider (a.k.a. the dependency injection container)
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "1st Book",
                        Description = "first description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        //Author = "FIrst Author",
                        CoverUrl = "http://...",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "Neuromancer",
                        Description = "SECOND description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-80),
                        Rate = 9,
                        Genre = "SciFi",
                        //Author = "William Gibson",
                        CoverUrl = "http://...",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "Neverwhere",
                        Description = "third description",
                        IsRead = false,
                        DateRead = null,
                        Rate = null,
                        Genre = "SciFi",
                        //Author = "Niel Gaimom",
                        CoverUrl = "http://...",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "The Vally of Fear",
                        Description = "Sherlock Holes Full  Length novel",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-90),
                        Rate = 8,
                        Genre = "Mystery",
                        //Author = "Sir Aurther Conan Doyle",
                        CoverUrl = "http://...",
                        DateAdded = DateTime.Now
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
