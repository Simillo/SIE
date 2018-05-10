using System;
using SIE.Context;

namespace SIE.Models
{
    public class MViewActivity
    {
        public MViewActivity() { }

        public MViewActivity(Activity activity)
        {
            Id = activity.Id;
            Name = activity.Title;
            ExpirationDate = activity.ExpirationDate;
            Description = activity.Description;
            CurrentState = activity.CurrentState;
            Weight = activity.Weight;
            EndDate = activity.EndDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int CurrentState { get; set; }
        public double Weight { get; set; }
    }
}
