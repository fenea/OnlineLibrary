using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User  // name, email, password
    {
        public string UserName{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid IdUser { get; set; }
        public string PhotoPath { get; set; }
        List<Book> Downloaded;
        List<Book> Saved;

        public User(string userName, string email, string password , string photoPath)
        {
            UserName = userName;
            Email = email;
            Password = password;
            PhotoPath = photoPath;

        }


    }
}
