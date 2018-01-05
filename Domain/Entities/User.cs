using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser  // name, email, password
    {
        public string PhotoPath { get; set; }

        public List<BookToReadUser> BookToReadUser { get; set; }
        public List<BookDownloadedUser> BookDownloadedUser { get; set; }
        public List<Rating> Ratings { get; set; }


        public string Role;

        public User()
        {
            BookDownloadedUser = new List<BookDownloadedUser>();
            BookToReadUser = new List<BookToReadUser>();
        }


    }
}
