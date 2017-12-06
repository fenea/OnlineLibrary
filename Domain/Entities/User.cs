using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
   public class User : IdentityUser // name, email, password
    {
        public string PhotoPath { get; set; }
        List<Book> Downloaded;
        List<Book> Saved;
    }
}
