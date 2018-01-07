using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Rating
    {

        public Rating()
        {

        }

        public String UserId { get; set; }
        public Guid BookId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }

        public int Grade { get; set; }
        public string Review { get; set; }


        public Rating(Book book, User user)
        {
            Book = book;
            User = user;

        }
        public static Rating Create(Book book,User user,string review,int grade)
        {
            Rating instance = new Rating(book, user);
            instance.Update(review, grade);
            return instance;
        }

        public void Update(string review, int grade)
        {
            Review = review;
            Grade = grade;
        }


    }
}