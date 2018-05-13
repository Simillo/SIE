using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Enums;

namespace SIE.Models
{
    public class MAllRoomsView
    {
        public MAllRoomsView()
        {
        }

        public MAllRoomsView(Room room, bool canIJoin)
        {
            Name = room.Name;
            Code = room.Code;
            CurrentState = "Aberta";
            NumberOfStudents = room.NumberOfStudents == 0 ? "-" : room.NumberOfStudents.ToString();
            CanIJoin = canIJoin;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string CurrentState { get; set; }
        public string NumberOfStudents { get; set; }
        public bool CanIJoin { get; set; }
    }
}
