using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class URelStudentRoom
    {
        private readonly SIEContext _context;
        public URelStudentRoom(SIEContext context) => _context = context;

        public List<int> GetRoomIdByPersonId(int personId) =>
            _context.RelStudentRoom
            .Where(r => r.PersonId == personId && r.Active)
            .Select(p => p.RoomId)
            .ToList();
    }
}
