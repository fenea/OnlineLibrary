using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class DatabaseContext : IdentityDbContext<User>, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }
        public DbSet<Domain.Entities.Book> Books { get; set; }
        //public DbSet<Rating> Ratings { get; set; }
       // public DbSet<Domain.Entities.User> Users { get; set; }

    }
}
