using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistance;

namespace Business
{

    public class RatingRepository : Domain.Interfaces.IRatingRepository
    {
        private readonly UserManager<User> _userManager;
        private DatabaseContext _db;



        public RatingRepository(UserManager<User> userManager, DatabaseContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public bool AddRating(User user, SeeReviewModel model)
        {
            try
            {
            Book _book = _db.Books.Find(model.Book.BookId);
            Rating rating = Rating.Create(_book, user, model._rating.Review, model._rating.Grade);


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
                return true;
            }
            catch
            {
                return false;
            }
        }

        public SeeReviewModel GetRatingsByBookId(string bookId)
        {

            Book _book = _db.Books.Find(new Guid(bookId));
            var ratings = _db.Ratings.Where(book => book.Book == _book);

            if (ratings.Count() == 0)
            {
                var model = new SeeReviewModel { Rating = new List<Rating>(), User = new List<User>(), Book = _book, Grades = new List<int>() };
                model.NrOfGradesOneProcent = 0;
                model.NrOfGradesTwoProcent = 0;
                model.NrOfGradesThreeProcent = 0;
                model.NrOfGradesFourProcent = 0;
                model.NrOfGradesFiveProcent = 0;
                return model;
            }
            else
            {
                List<User> users = new List<User>();
                List<int> grades = new List<int>();
                foreach (Rating r in ratings)
                {
                    string idUser = r.UserId;
                    var user = _db.Users.Find(idUser);
                    users.Add(user);
                    grades.Add(r.Grade);

                }

                var model = new SeeReviewModel { Rating = ratings.ToList(), User = users.ToList(), Book = _book, Grades = grades };
                model.NrOfGradesOneProcent = 100 * (ratings.Where(rating => rating.Grade == 1)).Count() / ratings.Count();
                model.NrOfGradesTwoProcent = 100 * (ratings.Where(rating => rating.Grade == 2)).Count() / ratings.Count();
                model.NrOfGradesThreeProcent = 100 * (ratings.Where(rating => rating.Grade == 3)).Count() / ratings.Count();
                model.NrOfGradesFourProcent = 100 * (ratings.Where(rating => rating.Grade == 4)).Count() / ratings.Count();
                model.NrOfGradesFiveProcent = 100 * (ratings.Where(rating => rating.Grade == 5)).Count() / ratings.Count();
                return model;
            }
        }
    }
}