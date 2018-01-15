using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Domain.Models;


namespace Presentation.Controllers
{

    public class HomeController : Controller
    {
        private IBookRepository _bookRepository;
        private DatabaseContext _db;
        private readonly UserManager<User> _userManager;

        public HomeController(IBookRepository bookRepository,DatabaseContext db,UserManager<User> userManager)
        {
            _bookRepository = bookRepository;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var idUser = _userManager.GetUserId(User);
            var currentUser = _db.Users.Find(idUser);

            int totalBooksToReadUser = _bookRepository.BooksToReadPerUser(idUser);
            int totalBooks = _bookRepository.GetAllBooks().Count();
            int booksDownloaded = _bookRepository.NrBooksDownloaded(currentUser);
            var topBooks = _bookRepository.TopBooks(6);
            var latesteBooks = _bookRepository.LatesteBooks(3);


            var model = new HomePageModel { BooksToReadNumber = totalBooksToReadUser, AllBooksNumber = totalBooks, BooksDownloaded=booksDownloaded,TopBooks = topBooks.ToList(),LatestBooks=latesteBooks.ToList() };

            return View(model);
        }
    }
}
