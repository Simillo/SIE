using System;
using SIE.Utils;

namespace SIE.Models
{
    public class MNewRoom
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }

        public bool ValidRoom(URoom uRoom)
        {
            var code = uRoom.SearchByCode(Code);
            return !string.IsNullOrEmpty(Name) && code == null;
        }
    }
}
