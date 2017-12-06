using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance;

//NUUUUUUU MERGE !!!!!!!!!!!!!!!!!!

namespace Business
{
    public class RatingRepository : Domain.Interfaces.IRattingRepository
    {
        private readonly IDatabaseContext _databaseService;

        public RatingRepository(IDatabaseContext databaseService)
        {
            _databaseService = databaseService;
        }

        public Rating GetRatingByUserIdAndBookId(Guid userId,Guid bookId)
        {
            return _databaseService.Ratings.FirstOrDefault(t => t.UserId == userId && t.BookId==bookId);

        }

        public IReadOnlyList<Rating> GetAllRatings()
        {
            IEnumerable<Rating> ratings = _databaseService.Ratings.Include("Rating");
            return ratings.ToList();
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

        public void DeleteRating(Guid bookId,Guid userId)
        {
            var rating = GetRatingByUserIdAndBookId(bookId,userId);
            _databaseService.Ratings.Remove(rating);
            _databaseService.SaveChanges();
        }

    
    }
 }

