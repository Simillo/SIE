using SIE.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIE.Models
{
    public class MAnswer
    {
        public MAnswer()
        {
        }

        public MAnswer(Answer answer)
        {
            Answer = answer?.UserAnswer;
            Grade = answer?.Grade;
            Feedback = answer?.Feedback;
        }

        public string Answer { get; set; }
        public double? Grade { get; set; }
        public string Feedback { get; set; }
        public List<string> Attachments { get; set; }
    }
}
