using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User:IdentityUser  // name, email, password
    {
        public string PhotoPath { get; set; }
        List<Book> Downloaded { get; set; }
        List<Book> Saved { get; set; }
        public string Role;

        public User()
        {

        }

        public User(string userName, string email, string password, string photoPath)
        {
               PhotoPath = photoPath;
        }


    }
}
