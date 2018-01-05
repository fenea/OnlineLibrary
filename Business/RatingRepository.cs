using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Business
{
    
    public class RatingRepository : Domain.Interfaces.IRatingRepository
    {
        private readonly IDatabaseContext _databaseService;

        public RatingRepository(IDatabaseContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Rating> GetAllRatings()
        {
            IEnumerable<Rating> ratings = _databaseService.Ratings.Include("Rating");
            return ratings.ToList();
        }

        public Rating GetRatingById(Guid id)
        {
            return _databaseService.Ratings.FirstOrDefault(t => t.BookId == id);
        }

        public void AddRating(Rating rating)
        {
            _databaseService.Ratings.Add(rating);
            _databaseService.SaveChanges();
        }

        public void EditRating(Rating rating)
        {
            _databaseService.Ratings.Update(rating);
            _databaseService.SaveChanges();
        }

        public void DeleteRating(Guid ratingid)
        {
            var user = GetRatingById(ratingid);
            _databaseService.Ratings.Remove(user);
            _databaseService.SaveChanges();
        }


    }
}
