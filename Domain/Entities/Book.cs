using System;
using System.Collections.Generic;

namespace Domain.Entities
{
   public class Book
    {
        public Guid Id;
        public string Name { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int Downloaded { get; set; }
        public string Path { get; set; }
        public string PhotoPath { get; set; }
        public List<Rating> Ratings;

        public double Score { get; set; }

        public Book(string name, string type, string author)
        {
            Name = name;
            Type = type;
            Author = author;
            Downloaded = 0;
        }
    }
}
