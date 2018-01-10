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

            List<User> users = new List<User>();

            foreach(Rating r in ratings)
            {
                string idUser = r.UserId;
                var user = _db.Users.Find(idUser);
                users.Add(user);

            }

            var model = new SeeReviewModel { Rating = ratings.ToList(), User=users.ToList(),Book = _book };

            if (ratings != null)
            {
                model.NrOfGradesOneProcent = 100 * (ratings.Where(rating => rating.Grade == 1)).Count() / ratings.Count();
                model.NrOfGradesTwoProcent = 100 * (ratings.Where(rating => rating.Grade == 2)).Count() / ratings.Count();
                model.NrOfGradesThreeProcent = 100 * (ratings.Where(rating => rating.Grade == 3)).Count() / ratings.Count();
                model.NrOfGradesFourProcent = 100 * (ratings.Where(rating => rating.Grade == 4)).Count() / ratings.Count();
                model.NrOfGradesFiveProcent = 100 * (ratings.Where(rating => rating.Grade == 5)).Count() / ratings.Count();
            }
            if (ratings == null)
            {
                model.NrOfGradesOneProcent = 0;
                model.NrOfGradesTwoProcent = 0;
                model.NrOfGradesThreeProcent = 0;
                model.NrOfGradesFourProcent = 0;
                model.NrOfGradesFiveProcent = 0;
              
            }
             

            return View(model);
        }

        /*
        public IActionResult SeeRatings(string id)
        {
            //string id = (string)TempData["Book"];
            Book _book = _db.Books.Find(new Guid(id));
            var ratings = _db.Ratings.Where(book => book.Book == _book);

            List<User> users = new List<User>();

            foreach (Rating r in ratings)
            {
                string idUser = r.UserId;
                var user = _db.Users.Find(idUser);
                users.Add(user);

            }

            var model = new SeeReviewModel { Rating = ratings.ToList(), User = users.ToList(), Book = _book };
            int total = 1;
            if (ratings != null)
            {
                total = ratings.Count();
            }
            model.NrOfGradesOneProcent = 100 * (ratings.Where(rating => rating.Grade == 1)).Count() / total;
            model.NrOfGradesTwoProcent = 100 * (ratings.Where(rating => rating.Grade == 2)).Count() / total;
            model.NrOfGradesThreeProcent = 100 * (ratings.Where(rating => rating.Grade == 3)).Count() / total;
            model.NrOfGradesFourProcent = 100 * (ratings.Where(rating => rating.Grade == 4)).Count() / total;
            model.NrOfGradesFiveProcent = 100 * (ratings.Where(rating => rating.Grade == 5)).Count() / total;



            return View(model);
        }
*/
     [HttpPost]
        public IActionResult AddRating(RatingModel model)
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
                return RedirectToAction("UpdateBook", new { id = id });
            }
            return RedirectToAction("Index", "Home");


        }
      
    }

}

