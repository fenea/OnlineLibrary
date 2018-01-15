
using Domain.Entities;
using System.Collections.Generic;
using System;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        IReadOnlyList<Book> GetAllBooks();
        Book GetBookById(Guid id);
        // void AddBook(CreateBookModel book);
        void AddBook(Book book);
        SeeAddedBooks TypeBooks(string type);
        int BooksToReadPerUser(string id);
        List<Book> BooksToReadPerUser(int nr);
        List<Book> LatesteBooks(int nr);
        List<Book> TopBooks(int nr);
        string AddBookToReadUser(User user, string bookId);
        SeeAddedBooks SeeAddedBooks(string userId);
        void AddBookToDownload(User user, string bookName);
        Book GetBookByName(string bookId);
        int NrBooksDownloaded(User user);

    }
}