﻿using System;
using SIE.Context;

namespace SIE.Models
{
    public class MRoomView
    {
        public MRoomView() { }

        public MRoomView(Room room)
        {
            Name = room.Name;
            Code = room.Code;
            ExpirationDate = room.ExpirationDate;
            Description = room.Description;
            CurrentState = room.CurrentState;
            NumberOfStudents = room.NumberOfStudents;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }
        public int CurrentState { get; set; }
        public int NumberOfStudents { get; set; }

    }
}