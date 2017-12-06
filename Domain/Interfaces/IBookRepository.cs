
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    interface IBookRepository
    {
        IReadOnlyList<Book> GetAllEager();
        IReadOnlyList<Book> GetAllExplicit();


        void AddBook(Book book);
        void EditStock(Book book);
        void DeleteStock(Book book);

    }
}
