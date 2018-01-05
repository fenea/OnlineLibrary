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



    }
}