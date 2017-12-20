using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Rating
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [ForeignKey("Book")]
        [Column(Order = 2)]
        public Guid BookId { get; set; }

        public int Grade { get; set; }
        public string Review { get; set; }

        public Rating(Guid bookId, Guid userId, int grade, string review)
        {
            BookId = bookId;
            UserId = userId;
            Grade = grade;
            Review = review;

        }
    }
}
