using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Persons.Data;
using Users.Models;

namespace Persons.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PersonsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PersonsContext>>()))
            {
                // Look for any user.
                if (context.User.Count() > 0)
                {
                    return;   // DB has been seeded
                }
                context.User.AddRange(
                    new User
                    {
                        FirstName = "Marco",
                        LastName = "Botton",
                        BirthDate = DateTime.Parse("1989-2-12")
                    },
                    new User
                    {
                        FirstName = "Mariah",
                        LastName = "Maclachlan",
                        BirthDate = DateTime.Parse("1979-3-14")
                    },
                    new User
                    {
                        FirstName = "Guido Jack",
                        LastName = "Guilizzoni",
                        BirthDate = DateTime.Parse("1965-5-19")
                    },
                    new User
                    {
                        FirstName = "Giuliano",
                        LastName = "Ravizzoni",
                        BirthDate = DateTime.Parse("1945-3-19"),
                        IsMarried = false
                    },
                    new User
                    {
                        FirstName = "Luzzio",
                        LastName = "Guilizzoni",
                        BirthDate = DateTime.Parse("1945-3-19"),
                        IsMarried = true,
                        Spouse = new User
                        {
                            FirstName = "Jenny",
                            LastName = "Lockley",
                            BirthDate = DateTime.Parse("1935-10-29")
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
