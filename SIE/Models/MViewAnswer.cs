using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;
using SIE.Migrations;

namespace SIE.Models
{
    public class MViewAnswer
    {

        public MViewAnswer(Answer answer)
        {
            Id = answer.Id;
            Answer = answer.UserAnswer;
            Grade = answer.Grade;
            SentDate = answer.SentDate;
            EvaluatedDate = answer.EvaluatedDate;
            Feedback = answer.Feedback;
            Name = answer.Person.Name;

        }
        public int Id { get; set; }
        public string Answer { get; set; }
        public double Grade { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? EvaluatedDate { get; set; }
        public string Feedback { get; set; }
        public string Name { get; set; }
    }
}
