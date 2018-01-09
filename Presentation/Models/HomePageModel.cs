﻿using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Presentation.Models
{
    public class HomePageModel
    {
        public int BooksToReadNumber { get; set; }
        public int AllBooksNumber { get; set; }
        public List<Book> AllBooks { get; set; }
    }
}