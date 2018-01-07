using System.Collections.Generic;
using Domain.Entities;

namespace Presentation.Models
{
    public class SeeReviewModel
    {
        public  List<Rating> Rating { get;set;}
        public  Book Book { get;set;}

    }
}
