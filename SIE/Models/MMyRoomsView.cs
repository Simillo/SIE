using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Enums;

namespace SIE.Models
{
    public class MMyRoomsView
    {
        public MMyRoomsView()
        {
        }

        public MMyRoomsView(Room room)
        {
            Name = room.Name;
            Code = room.Code;
            CurrentState = ((ERoomState) room.CurrentState).Description();
            NumberOfStudents = room.NumberOfStudents == 0 ? "-" : room.NumberOfStudents.ToString();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string CurrentState { get; set; }
        public string NumberOfStudents { get; set; }
    }
}
