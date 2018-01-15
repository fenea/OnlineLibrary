using System.Collections.Generic;
using Domain.Entities;

namespace Business.Models
{
    public class SeeReviewModel
    {
        public List<Rating> Rating { get; set; }
        public Rating _rating { get; set; }
        public List<User> User { get; set; }
        public List<int> Grades { get; set; }
        public Book Book { get; set; }
        public int NrOfGradesOneProcent { get; set; }
        public int NrOfGradesTwoProcent { get; set; }
        public int NrOfGradesThreeProcent { get; set; }
        public int NrOfGradesFourProcent { get; set; }
        public int NrOfGradesFiveProcent { get; set; }



    }
}
