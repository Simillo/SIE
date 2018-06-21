using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Models
{
    public class MViewActivity
    {
        public MViewActivity(Activity activity, Answer answer = null, List<Answer> answers = null, List<RelUploadActivity> uploads = null)
        {
            Id = activity.Id;
            Name = activity.Title;
            ExpirationDate = activity.ExpirationDate;
            Description = activity.Description;
            CurrentState = activity.CurrentState;
            Weight = activity.Weight;
            EndDate = activity.EndDate;
            Answer = new MAnswer(answer);
            Uploads = uploads?.Select(u => u.Document.FileName).ToList() ?? new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int CurrentState { get; set; }
        public double Weight { get; set; }
        public MAnswer Answer { get; set; }
        public List<MViewAnswer> Answers { get; set; }
        public List<string> Uploads { get; set; }
    }
}
