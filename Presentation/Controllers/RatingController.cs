using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Models;
using Persistance;
using Domain.Interfaces;
using System.Diagnostics.Contracts;


namespace Presentation.Controllers
{
    public class RatingController:Controller
    {
        private readonly UserManager<User> _userManager;
        private DatabaseContext _db;
        private IRatingRepository _ratingRepository;



        public RatingController( UserManager<User> userManager, DatabaseContext db,IRatingRepository ratingRepository)
        {
            _userManager = userManager;
            _ratingRepository = ratingRepository;
            _db = db;
        }


        //se apeleaza inainte de seeRatings
        public IActionResult UpdateBook(string id)
        {
            TempData["Book"] = id;
            return RedirectToAction("SeeRatings");

        }


        public IActionResult SeeRatings()
        {
            string id = (string)TempData["Book"];
            var model=_ratingRepository.GetRatingsByBookId(id);
            return View(model);

        }

     
        [HttpPost]
        public IActionResult AddRatings(SeeReviewModel model)
        {
            Contract.Ensures(Contract.Result<IActionResult>() != null);
            if (ModelState.IsValid)
            {
                var idUser = _userManager.GetUserId(User);
                var user = _db.Users.Find(idUser);
               
                if(_ratingRepository.AddRating(user,model)==false)
                {

                    TempData["error"] = "You already added a comment";
                }
                return RedirectToAction("UpdateBook", new { id = model.Book.BookId });
            }
            return RedirectToAction("UpdateBook", new { id = model.Book.BookId });


        }
      
    }

}

