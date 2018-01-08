using System.Collections.Generic;
using Domain.Entities;

namespace Presentation.Models
{
    public class SeeReviewModel
    {
        public  List<Rating> Rating { get;set;}
        public  List<User> User { get; set; }
        public  Book Book { get;set;}
        public int NrOfGradesOneProcent { get; set; }
        public int NrOfGradesTwoProcent { get; set; }
        public int NrOfGradesThreeProcent { get; set; }
        public int NrOfGradesFourProcent { get; set; }
        public int NrOfGradesFiveProcent { get; set; }


    }
}
