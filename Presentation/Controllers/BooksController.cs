using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Persistance;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Domain.Models;
using System;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _bookRepository;
        private DatabaseContext _db;
        private readonly UserManager<User> _userManager;


        public BooksController(IBookRepository bookRepository,UserManager<User> userManager, DatabaseContext db)
        {
            _bookRepository = bookRepository;
            _userManager = userManager;
            _db = db;

        }

        public IActionResult ThrillerBooks()
        {
            return View(_bookRepository.TypeBooks("Thriller"));
        }

        public IActionResult RomanceBooks()
        {
            return View(_bookRepository.TypeBooks("Romance"));
        }


        public IActionResult DramaBooks()
        {
            return View(_bookRepository.TypeBooks("Drama"));

        }


        public IActionResult ActionBooks()
        {
            return View(_bookRepository.TypeBooks("Action"));
        }

     
        [Authorize]
        public IActionResult AddBook()
        {
            return View();
        }


       /* [HttpPost]
        public IActionResult AddBook(CreateBookModel model)
        {

            if(ModelState.IsValid)
            {
                _bookRepository.AddBook(model);
            }

            return RedirectToAction("Index", "Home");

        }*/



        [HttpPost]
        public IActionResult AddBook(CreateBookModel model)
        {

            if(ModelState.IsValid)
            {

                Book book = Book.Create(model.Name, model.Type, model.Author, model.Path, model.PhotoPath, model.Description,DateTime.Now);
                _bookRepository.AddBook(book);
            }

            return RedirectToAction("Index", "Home");

        }


 
        public IActionResult Download(string name)
        {
            
            var idUser = _userManager.GetUserId(User);
            var user = _db.Users.Find(idUser);
            _bookRepository.AddBookToDownload(user, name);
            string fileName = name + ".pdf";
            string fullName ="Books/" + fileName;
            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
            {
                throw new IOException(s);
            }
            return data;
        }


        public ActionResult ReadPdf(string name)
        {
            string path = "Books/" + name + ".pdf";
            string fileName = name + ".pdf";
            var fileStream = new FileStream(path,FileMode.Open,FileAccess.Read);
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;

        }

        public JsonResult AddToReadBooks(string id)
        {

            var idUser = _userManager.GetUserId(User);
            var user = _db.Users.Find(idUser);
            string result= _bookRepository.AddBookToReadUser(user, id);
            return Json(new { isAdded = result });
        

        }

        public IActionResult SeeAddedBooks()
        {
            var idUser = _userManager.GetUserId(User);
            var model=_bookRepository.SeeAddedBooks(idUser);

            return View(model);
        } 

    }
}