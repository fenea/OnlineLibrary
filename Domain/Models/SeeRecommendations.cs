using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Models
{
    public class SeeRecommendations
    {
        public List<Book> BooksToReadUser { get; set; }
        public string error { get; set; }

    }
}
