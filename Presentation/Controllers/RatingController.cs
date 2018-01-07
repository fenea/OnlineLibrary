using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Entities;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Persistance;
using System.Linq;

namespace Presentation.Controllers
{
    public class RatingController:Controller
    {
        private readonly UserManager<User> _userManager;
        private DatabaseContext _db;
      


        public RatingController( UserManager<User> userManager, DatabaseContext db)
        {
            _userManager = userManager;
            _db = db;
        }


        //apelez UpdateBookId prima
        public IActionResult UpdateBookId(string id)
        {
            TempData["id"]  = id;
            return RedirectToAction("AddRating");

        }

      
        public IActionResult AddRating()
        {
            return View();


        }

        public IActionResult UpdateBook(string id)
        {
            TempData["Book"] = id;
            return RedirectToAction("SeeRatings");

        }

        public IActionResult SeeRatings()
        {
            string id = (string)TempData["Book"];
            Book _book = _db.Books.Find(new Guid(id));
            var ratings = _db.Ratings.Where(book => book.Book == _book);
            var model = new SeeReviewModel { Rating = ratings.ToList(), Book = _book };
           
            return View(model);
        }

        /*
        public ViewResult SeeRatings(string id)
        {
            //string id = (string)TempData["Book"];
            Book _book = _db.Books.Find(new Guid(id));
            var Ratings = _db.Ratings.Where(book => book.Book == _book);
            var model = new SeeReviewModel { Rating = Ratings.ToList(), Book = _book };
            return View(model);

        }
*/
        [HttpPost]
        public void AddRating(RatingModel model)
        {
            if (ModelState.IsValid)
            {
                string id = (string)TempData["id"];
                var idUser = _userManager.GetUserId(User);
                var user = _db.Users.Find(idUser);
                Book _book = _db.Books.Find(new Guid(id));
                Rating rating = Rating.Create(_book, user, model.Review, model.Grade);
                _book.Ratings.Add(rating);
                _db.Books.Update(_book);
                _db.SaveChanges();

                int score = 0;
                var ratings = _db.Ratings.Where(book => book.Book == _book);
                foreach (Rating rate in ratings)
                {
                    score += rate.Grade;
                }

                _book.Score = score / ratings.Count();

                _db.Books.Update(_book);
                _db.SaveChanges();

            }

        }
      
    }

}

