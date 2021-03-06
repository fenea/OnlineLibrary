﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistance
{
    public interface IDatabaseContext
    {
        EntityEntry Entry(object entity);
        DbSet<Book> Books { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<BookToReadUser> BooksToReadUser { get; set; }

        int SaveChanges();
    }
}
