using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IRatingRepository
    {
        bool AddRating(User user,SeeReviewModel model );
        SeeReviewModel GetRatingsByBookId(string bookId);

    }
}
