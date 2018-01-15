using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }


    }
}
