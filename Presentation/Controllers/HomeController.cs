using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
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

        public HomeController(IBookRepository bookRepository,DatabaseContext db)
        {
            _bookRepository = bookRepository;
            _db = db;
        }

        public IActionResult Index()
        {
            
            var id = TempData["userId"];
            TempData.Keep("userId");
            var currentUser = _db.Users.Find(id);
          
            ViewData["DownloadedBooks"]=currentUser.BookDownloadedUser.Count();
            ViewData["BooksToRead"] = currentUser.BookToReadUser.Count();
            ViewData["AllBooks"] = _bookRepository.GetAllBooks().Count();

            var res = _bookRepository.GetAllBooks();

            return View(res);
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
