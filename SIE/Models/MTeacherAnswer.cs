using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIE.Models
{
    public class MTeacherAnswer
    {
        public int Id { get; set; }
        public double Grade { get; set; }
        public DateTime? EvaluatedDate { get; set; }
        public string Feedback { get; set; }
    }
}
