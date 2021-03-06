﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser  
    {
        public string PhotoPath { get; set; }

        public List<BookToReadUser> BookToReadUser { get; set; }
        public List<BookDownloadedUser> BookDownloadedUser { get; set; }
        public List<Rating> Ratings { get; set; }
        public int Nr { get; set; }


        public User()
        {
            Ratings = new List<Rating>();
            BookDownloadedUser = new List<BookDownloadedUser>();
            BookToReadUser = new List<BookToReadUser>();
        }


    }
}
