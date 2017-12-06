using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    class User : IdentityUser // name, email, password
    {
        List<Book> Downloaded;
        List<Book> Saved;

    }
}
