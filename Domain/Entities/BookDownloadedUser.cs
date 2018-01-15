﻿using System;

namespace Domain.Entities
{
    public class BookDownloadedUser
    {

        public BookDownloadedUser()
        {
            
        }


        public BookDownloadedUser(User user,Book book)
        {
            User = user;
            Book = book;
        }


        public String Id { get; set; }
        public User User { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }

    }
}
