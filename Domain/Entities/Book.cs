using System;

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

        public double Rating { get; set; }

        public Book(string name, string type, string author)
        {
            Name = name;
            Type = type;
            Author = author;
            Downloaded = 0;
        }
    }
}
