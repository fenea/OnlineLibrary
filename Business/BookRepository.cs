using Domain.Entities;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Interfaces;

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
        public List<Book>GetDownloadedBooksByType(User user,string type)
        {
            var BookDownloadedByUser=_databaseService.BooksDownloadedUser.Where(u => u.Id == user.Id).ToList();
            List<Book> books = new List<Book>();
           
            foreach(var b in BookDownloadedByUser)
            {
                books.Add(_databaseService.Books.First(book => book.BookId == b.BookId));
            }

            if (type != null)
            {
                return books.Where(b => b.Type == type).ToList();
            }
            else
                return books.ToList();


        }


        public Book GetTopFromEachType(string type)
        {

            var books = _databaseService.Books.Where(b => b.Type == type).OrderByDescending(b => b.Score).ToList();
            return books[0];
        }

        public string MostDownloadedBookTypeByUser(User user)
        {


            List<int> count = new List<int>();
            List<string> types = new List<string>();

            count.Add(GetDownloadedBooksByType(user, "Action").Count());
            types.Add("Action");
            count.Add(GetDownloadedBooksByType(user, "Drama").Count());
            types.Add("Drama");
            count.Add(GetDownloadedBooksByType(user, "Thriller").Count());
            types.Add("Thriller");
            count.Add(GetDownloadedBooksByType(user, "Romance").Count());
            types.Add("Romance");


            int max = 0;
            string type = null;

            for (int i = 0; i < count.Count();i++)
            {
                if (count[i] > max)
                {
                    max = count[i];
                    type = types[i];
                }
            }
            return type;
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


        public SeeRecommendations RecommendBooks(User user)
        {
            try
            {
                List<Book> downloadedbooksUser = GetDownloadedBooksByType(user, null);
                List<Book> listBooksToRecommend = new List<Book>();
                string mostDownloadedType = MostDownloadedBookTypeByUser(user);

                if (mostDownloadedType.Equals("Action"))
                {
                    listBooksToRecommend.AddRange(_databaseService.Books.Where(b => b.Type == "Action").OrderByDescending(b => b.Score).Take(5));
                    listBooksToRecommend.Add(GetTopFromEachType("Drama"));
                    listBooksToRecommend.Add(GetTopFromEachType("Romance"));
                    listBooksToRecommend.Add(GetTopFromEachType("Thriller"));
                }

                if (mostDownloadedType.Equals("Drama"))
                {
                    listBooksToRecommend.AddRange(_databaseService.Books.Where(b => b.Type == "Drama").OrderByDescending(b => b.Score).Take(5));
                    listBooksToRecommend.Add(GetTopFromEachType("Action"));
                    listBooksToRecommend.Add(GetTopFromEachType("Romance"));
                    listBooksToRecommend.Add(GetTopFromEachType("Thriller"));
                }

                if (mostDownloadedType.Equals("Thriller"))
                {
                    listBooksToRecommend.AddRange(_databaseService.Books.Where(b => b.Type == "Thriller").OrderByDescending(b => b.Score).Take(5));
                    listBooksToRecommend.Add(GetTopFromEachType("Drama"));
                    listBooksToRecommend.Add(GetTopFromEachType("Romance"));
                    listBooksToRecommend.Add(GetTopFromEachType("Action"));
                }

                if (mostDownloadedType.Equals("Romance"))
                {
                    listBooksToRecommend.AddRange(_databaseService.Books.Where(b => b.Type == "Romance").OrderByDescending(b => b.Score).Take(5));
                    listBooksToRecommend.Add(GetTopFromEachType("Drama"));
                    listBooksToRecommend.Add(GetTopFromEachType("Action"));
                    listBooksToRecommend.Add(GetTopFromEachType("Thriller"));
                }


                foreach (Book book in listBooksToRecommend.Reverse<Book>())
                {
                    foreach (Book bookDownloaded in downloadedbooksUser)
                    {
                        if (book.Name.Equals(bookDownloaded.Name))
                        {
                            listBooksToRecommend.Remove(book);

                        }
                    }
                }

                var model = new SeeRecommendations { BooksToReadUser = listBooksToRecommend.ToList(),error=null };
                return model;
            }
            catch
            {
                var model = new SeeRecommendations { BooksToReadUser = null,error="Download at least one book in order to see recommendatins"};
                return model;
            }

        }

      
    }
}