﻿using System;
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
            Answers = answers?.Select(a => new MViewAnswer(a)).ToList() ?? new List<MViewAnswer>();
            Uploads = uploads?.Select(u => new MViewDocument(u.Document)).ToList() ?? new List<MViewDocument>();
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
        public List<MViewDocument> Uploads { get; set; }
    }
}
