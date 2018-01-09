using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Presentation.Models;
using Persistance;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Presentation.Controllers {     public class AccountController : Controller     {         private readonly ILogger<AccountController> _logger;         private readonly SignInManager<User> _signInManager;         private readonly UserManager<User> _userManager;         private DatabaseContext _db;


         public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager, UserManager<User> userManager, DatabaseContext db)         {             _logger = logger;
            _signInManager = signInManager;             _userManager = userManager;
            _db = db;         }          private void AddErrors(IdentityResult result)         {             foreach (var error in result.Errors)             {                 ModelState.AddModelError("", error.Description);             }         }          public IActionResult Login()         {             if (this.User.Identity.IsAuthenticated)             {                 return RedirectToAction("Index");             }              return View();         }

        [HttpPost]         public async Task<IActionResult> Login(LoginViewModel model)         {             if (ModelState.IsValid)             {
                //allow to signin only with username+password
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);



                if (result.Succeeded)                 {
                    TempData["userId"] = model.Username;
                    return RedirectToAction("Index");
                }              }              ModelState.AddModelError("", "Failed to login ");              return View();         }          [HttpGet]         public async Task<IActionResult> Logout()         {             await _signInManager.SignOutAsync();             return RedirectToAction("Login");         }          public IActionResult Register()         {             return View();         }

        public IActionResult Index()
        {
            var currentUserId = _userManager.GetUserId(User);           
            TempData["userId"] = currentUserId;//currentUser.Id;
            return RedirectToAction("Index", "Home");
        }


        public JsonResult AddToReadBooks(string id)
        {

            var idUser = _userManager.GetUserId(User);
            var user =_db.Users.Find(idUser);
            Book book = _db.Books.Find(new Guid(id));
            user.BookToReadUser.Add(new BookToReadUser(user, book));
           
           try
            {
                _userManager.UpdateAsync(user);
                _db.SaveChanges();

            }
            catch
            {
                return Json(new { isAdded = id ,numar=user.BookToReadUser.Count() });
            }
            return Json(new { isAdded = "success"});

        }

        public IActionResult SeeAddedBooks()
        {
            var idUser = _userManager.GetUserId(User);
            var books = _db.BooksToReadUser.Where(user => user.Id == idUser);

            List<Book> booksToReadUser = new List<Book>();

            foreach (var b in books)
            {
                Book book = _db.Books.Find(b.BookId);
                booksToReadUser.Add(book);

            }

            var model = new SeeAddedBooks { BooksToReadUser = booksToReadUser.ToList() };

            return View(model);
        } 



        [HttpPost]         public async Task<IActionResult> Register(RegisterViewModel model)         {             if (ModelState.IsValid)             {
                 var user = new User { UserName = model.Username, Email = model.Email };



                var result = await _userManager.CreateAsync(user, model.Password);

              
                if (result.Succeeded)                 {                     await _signInManager.SignInAsync(user, isPersistent: false);                     return RedirectToAction("Index");                 }
                AddErrors(result);             }


            return View();         }     } } 