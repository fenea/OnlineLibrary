using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Presentation.Models;



//nr carti site , cate carti desc per user , cate carti are ptr to read

namespace Presentation.Controllers
{

    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
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

            int totalBooksToReadUser = (_db.BooksToReadUser.Where(user => user.Id == idUser)).Count();
            int totalBooks = _bookRepository.GetAllBooks().Count();
            var topBooks = _bookRepository.GetAllBooks().OrderByDescending(b => b.Score).Take(6);
            var latesteBooks = _bookRepository.GetAllBooks().OrderByDescending(b => b.Added).Take(3);


            var model = new HomePageModel { BooksToReadNumber = totalBooksToReadUser, AllBooksNumber = totalBooks, TopBooks = topBooks.ToList(),LatestBooks=latesteBooks.ToList() };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
