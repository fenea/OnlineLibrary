using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRatingRepository
    {
            IReadOnlyList<Rating> GetAllRatings();
            Rating GetRatingByUserIdAndBookId(Guid userId, Guid bookId);
            void AddRating(Rating rating);
            void EditRating(Rating rating);
            void DeleteRating(Guid bookId, Guid userId);

      }
}

