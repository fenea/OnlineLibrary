using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Entities;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult AdBook(Book model)
        {
            if(ModelState.IsValid)
            {
                _bookRepository.AddBook(model);
            }
          
                return View("ListBooks");
            
        }

        
        // POST api/values
        [HttpPost("/post/BooksController")]
        public void Post([FromBody]CreateBookModel book)
        {
            var entity = Book.Create(book.Name, book.Type , book.Path ,book.PhotoPath , book.Author);
            _bookRepository.AddBook(entity);
           
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]UpdateBookModel book)
        {
            var entity = _bookRepository.GetBookById(id);
            entity.Update(book.Name, book.Type, book.Path, book.PhotoPath, book.Author);
            _bookRepository.EditBook(entity);
        }


        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _bookRepository.DeleteBook(id);
        }
    }
}