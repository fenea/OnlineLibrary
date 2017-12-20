
using Domain.Entities;
using System.Collections.Generic;
using System;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        IReadOnlyList<Book> GetAllBooks();
        Book GetBookById(Guid id);
        void AddBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Guid idBook);


    }
}
