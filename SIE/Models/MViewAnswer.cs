using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Models
{
    public class MViewAnswer
    {

        public MViewAnswer(Answer answer, List<RelUploadAnswer> attachments)
        {
            Id = answer.Id;
            Answer = answer.UserAnswer;
            Grade = answer.Grade;
            SentDate = answer.SentDate;
            EvaluatedDate = answer.EvaluatedDate;
            Feedback = answer.Feedback;
            Name = answer.Person.Name;
            Attachments = attachments.Select(a => a.Document.FileName).ToList();
        }
        public int Id { get; set; }
        public string Answer { get; set; }
        public double Grade { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? EvaluatedDate { get; set; }
        public string Feedback { get; set; }
        public string Name { get; set; }
        public List<string> Attachments { get; set; }
    }
}
