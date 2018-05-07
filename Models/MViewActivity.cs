using System;
using SIE.Context;

namespace SIE.Models
{
    public class MViewActivity
    {
        public MViewActivity() { }

        public MViewActivity(Activity activity)
        {
            Name = activity.Title;
            ExpirationDate = activity.ExpirationDate;
            Description = activity.Description;
            CurrentState = activity.CurrentState;
            Weight = activity.Weight;
        }

        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }
        public int CurrentState { get; set; }
        public double Weight { get; set; }
    }
}
