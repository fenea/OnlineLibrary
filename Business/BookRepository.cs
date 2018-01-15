using Domain.Entities;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class BookRepository : Domain.Interfaces.IBookRepository
    {
        private readonly DatabaseContext _databaseService;
        private readonly UserManager<User> _userManager;


        public BookRepository(DatabaseContext databaseService,UserManager<User> userManager)
        {
            _databaseService = databaseService;
            _userManager = userManager;
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _databaseService.Books.ToList();
        }

        public Book GetBookById(Guid id)
        {
            return _databaseService.Books.FirstOrDefault(t => t.BookId == id);
        }

        /*
        public void AddBook(CreateBookModel model)
        {
            string FilePath = "Books\\" + model.Name + ".pdf";
            string PhotoPath = "wwwroot\\images\\books\\" + model.Type + "\\" + model.Name + ".jpg";
            Console.WriteLine(model.BookFile.FileName);

            if (model.BookFile.Length > 0)
            {
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.BookFile.CopyTo(fileStream);
                }
            }
            if (model.PhotoPath.Length > 0)
            {
                using (var fileStream = new FileStream(PhotoPath, FileMode.Create))
                {
                    model.PhotoPath.CopyTo(fileStream);
                }
            }

            Book book = Book.Create(model.Name, model.Type, model.Author, FilePath, PhotoPath, model.Description, DateTime.Now);
            _databaseService.Books.Add(book);
            _databaseService.SaveChanges();

        }
        */

        public void AddBook(Book book)
        {
            _databaseService.Books.Add(book);
            _databaseService.SaveChanges();
        }

        public SeeAddedBooks TypeBooks(string type)
        {

            var model = new SeeAddedBooks { BooksToReadUser = GetAllBooks().Where(b => b.Type == type).ToList() };

            return model;

        }

        public int BooksToReadPerUser(string id)
        {
            return _databaseService.BooksToReadUser.Where(user => user.Id == id).Count();

        }


        public List<Book> LatesteBooks(int nr)
        {
            return GetAllBooks().OrderByDescending(b => b.Added).Take(nr).ToList();
        }

        public List<Book> BooksToReadPerUser(int nr)
        {
            throw new NotImplementedException();
        }

        public List<Book> TopBooks(int nr)
        {
            return GetAllBooks().OrderByDescending(b => b.Score).Take(nr).ToList();        
        }

        public string AddBookToReadUser(User user, string bookId)
        {
            Book book = _databaseService.Books.Find(new Guid(bookId));
            user.BookToReadUser.Add(new BookToReadUser(user, book));

            try
            {
                _userManager.UpdateAsync(user);
                _databaseService.SaveChanges();
                return ("Book was added");
            }
            catch
            {
                return ("Book was already added");
            }
        }

        public SeeAddedBooks SeeAddedBooks(string userId)
        {
            var books = _databaseService.BooksToReadUser.Where(user => user.Id == userId);

            List<Book> booksToReadUser = new List<Book>();

            foreach (var b in books)
            {
                Book book = _databaseService.Books.Find(b.BookId);
                booksToReadUser.Add(book);

            }

            var model = new SeeAddedBooks { BooksToReadUser = booksToReadUser.ToList() };
             
            return model;
        }

        public int NrBooksDownloaded(User user)
        {
            return _databaseService.BooksDownloadedUser.Where(u => u.Id == user.Id).Count();
        }

        public void AddBookToDownload(User user, string bookName)
        {
            Book book = GetBookByName(bookName);

            if (_databaseService.BooksDownloadedUser.Where(u => u.Id == user.Id).Where(u => u.Book==book).Count()==0)
            {
                user.Nr = 1;
                user.BookDownloadedUser.Add(new BookDownloadedUser(user, book));
            }
            //cartea este in list to reads elimin.o
            var books = _databaseService.BooksToReadUser.Where(u => u.Id == user.Id);


            foreach (var b in books)
            {
                if(b.BookId==book.BookId)
                    user.BookToReadUser.Remove(b);

            }

            _databaseService.SaveChanges(); 

        }

        public Book GetBookByName(string bookName)
        {
            return _databaseService.Books.First(b => b.Name == bookName);
        }


    }
}