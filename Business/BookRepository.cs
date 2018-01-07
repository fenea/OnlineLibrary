using Domain.Entities;
using Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class BookRepository : Domain.Interfaces.IBookRepository
    {
        private readonly DatabaseContext _databaseService;

        public BookRepository(DatabaseContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _databaseService.Books.ToList();
        }

        public Book GetBookById(Guid id)
        {
            return _databaseService.Books.FirstOrDefault(t => t.BookId == id);
        }

        public void AddBook(Book book)
        {
            _databaseService.Books.Add(book);
            _databaseService.SaveChanges();
        }

        public void EditBook(Book book)
        {
            _databaseService.Books.Update(book);
            _databaseService.SaveChanges();
        }

        public void DeleteBook(Guid bookid)
        {
            var book = GetBookById(bookid);
            _databaseService.Books.Remove(book);
            _databaseService.SaveChanges();
        }

   



    }
}