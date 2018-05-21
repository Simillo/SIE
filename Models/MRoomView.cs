using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Models
{
    public class MRoomView
    {
        public MRoomView() { }

        public MRoomView(Room room, IEnumerable<Activity> activities, IEnumerable<Answer> answers = null)
        {
            Name = room.Name;
            Code = room.Code;
            ExpirationDate = room.ExpirationDate;
            Description = room.Description;
            CurrentState = room.CurrentState;
            NumberOfStudents = room.NumberOfStudents;
            EndDate = room.EndDate;
            Activities = activities.Select(a => new MViewActivity(a, answers?.FirstOrDefault(x => x.Activity.Id == a.Id)));
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int CurrentState { get; set; }
        public int NumberOfStudents { get; set; }
        public IEnumerable<MViewActivity> Activities { get; set; }
    }
}
