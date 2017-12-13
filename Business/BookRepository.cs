﻿using Domain.Entities;
using Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class BookRepository : Domain.Interfaces.IBookRepository
    {
        private readonly IDatabaseContext _databaseService;

        public BookRepository(IDatabaseContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            IEnumerable<Book> books = _databaseService.Books.Include("Book");
            return books.ToList();
        }

        public Book GetBookById(Guid id)
        {
            return _databaseService.Books.FirstOrDefault(t => t.Id == id);
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