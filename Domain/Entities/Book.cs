using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int Downloaded { get; set; }
        public string Path { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<BookDownloadedUser> BookDownloadedUser { get; set; }
        public List<BookToReadUser> BookToReadUser { get; set; }


        public double Score { get; set; }

        public Book()
        {
            Ratings = new List<Rating>();
            BookDownloadedUser = new List<BookDownloadedUser>();
            BookToReadUser = new List<BookToReadUser>();
        }

        public static Book Create(string name, string type, string author, string path, string photoPath, string description, DateTime added)
        {
            var instance = new Book { BookId = Guid.NewGuid() };
            instance.Update(name, type, author, path, photoPath, description, added);
            return instance;
        }

        public void Update(string name, string type, string author, string path, string photoPath, string description, DateTime added)
        {
            Name = name;
            Type = type;
            Author = author;
            Downloaded = 0;
            Path = path;
            PhotoPath = photoPath;
            Description = description;
            Added = added;
        }

    }
}
