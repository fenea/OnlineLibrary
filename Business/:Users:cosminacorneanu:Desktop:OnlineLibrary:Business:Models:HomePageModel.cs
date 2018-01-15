using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Business.Models
{
    public class HomePageModel
    {
        public int BooksToReadNumber { get; set; }
        public int AllBooksNumber { get; set; }
        public List<Book> TopBooks { get; set; }
        public List<Book> LatestBooks { get; set; }

    }
}