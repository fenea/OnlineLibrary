using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Entities;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using System.Web;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("/getall/BooksController")]
        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public IActionResult ListBooks()
        {
            var res = _bookRepository.GetAllBooks();
            return View(res);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book GetBookById(Guid id)
        {
            return _bookRepository.GetBookById(id);
        }

        [Authorize]
        public IActionResult AddBook()
        {
            return View();

        }

      

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

        


        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]UpdateBookModel book)
        {
            var entity = _bookRepository.GetBookById(id);
            entity.Update(book.Name, book.Type, book.Path, book.PhotoPath, book.Author, book.Description,DateTime.Now);
            _bookRepository.EditBook(entity);
        }


        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _bookRepository.DeleteBook(id);
        }

        public IActionResult Download(string name)
        {
            string fileName = name + ".pdf";

            string fullName ="Books\\" + fileName;
            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}