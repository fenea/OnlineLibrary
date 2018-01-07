using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class RatingModel
    {
        [Required]
        public string Review { get; set; }
        [Required]
        [Range(1, 6)]
        public int Grade { get; set; }
    }
}
