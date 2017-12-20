using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int Downloaded { get; set; }
        public string Path { get; set; }
        public string PhotoPath { get; set; }
        public List<Rating> Ratings { get; set; }

        public double Score { get; set; }

        public Book()
        {

        }
        public Book(string name, string type, string author, string path, string photoPath)
        {
            Name = name;
            Type = type;
            Author = author;
            Downloaded = 0;
            Path = path;
            PhotoPath = photoPath;
        }

        public static Book Create(string name, string type, string author,string path, string photoPath)
        {
            var instance = new Book { Id = Guid.NewGuid() };
            instance.Update(name, type,author,path,photoPath);
            return instance;
        }

        public void Update(string name, string type, string author, string path, string photoPath)
        {
            Name = name;
            Type = type;
            Author = author;
            Downloaded = 0;
            Path = path;
            PhotoPath = photoPath;
        }

    }
}
