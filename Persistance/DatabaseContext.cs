using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistance
{

    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<BookToReadUser> BooksToReadUser { get; set; }
        public DbSet<BookDownloadedUser> BooksDownloadedUser { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDownloadedUser>()
                        .HasKey(t => new { t.Id, t.BookId });

            modelBuilder.Entity<BookDownloadedUser>()
                .HasOne(pt => pt.Book)
                .WithMany(p => p.BookDownloadedUser)
                .HasForeignKey(pt => pt.BookId);

            modelBuilder.Entity<BookDownloadedUser>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.BookDownloadedUser)
                .HasForeignKey(pt => pt.Id);


            modelBuilder.Entity<BookToReadUser>()
                   .HasKey(t => new { t.Id, t.BookId });

            modelBuilder.Entity<BookToReadUser>()
                .HasOne(pt => pt.Book)
                .WithMany(p => p.BookToReadUser)
                .HasForeignKey(pt => pt.BookId);

            modelBuilder.Entity<BookToReadUser>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.BookToReadUser)
                .HasForeignKey(pt => pt.Id);


            modelBuilder.Entity<Rating>()
                        .HasKey(t => new { t.UserId, t.BookId });

            modelBuilder.Entity<Rating>()
                    .HasOne(m => m.Book)
                    .WithMany(t => t.Ratings)
                    .HasForeignKey(m => m.BookId);

            modelBuilder.Entity<Rating>()
                    .HasOne(m => m.User)
                    .WithMany(t => t.Ratings)
                        .HasForeignKey(m => m.UserId);

            base.OnModelCreating(modelBuilder);
        }

    }


}
