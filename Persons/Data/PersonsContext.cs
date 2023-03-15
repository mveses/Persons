using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Persons.Data
{
    public class PersonsContext : DbContext
    {
        public PersonsContext (DbContextOptions<PersonsContext> options)
            : base(options)
        {

        }

        public DbSet<Users.Models.User> User { get; set; } = default!;
        public DbSet<Users.Models.Addresses> Addresses { get; set; } = default!;    
        public DbSet<Users.Models.PhoneNumbers> PhoneNumbers { get; set; } = default!;
    }
}
