using System;
namespace Domain.Entities
{
    public class Rating
    {
        public Guid UserId;
        public Guid BookId;
        public int Grade { get; set; }
        public string Review { get; set; }

        public Rating(Guid bookId,Guid userId , int grade , string review)
        {
            BookId = bookId;
            UserId = userId;
            Grade = grade;
            Review = review;

        }
    }
}
