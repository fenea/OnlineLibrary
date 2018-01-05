using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRatingRepository
    {
        
        IReadOnlyList<Rating> GetAllRatings();
        Rating GetRatingById(Guid id);
        void AddRating(Rating rating);
        void EditRating(Rating rating);
        void DeleteRating(Guid irating);



    }
}
