using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Utils
{
    public class URoom
    {
        private readonly SIEContext _context;

        public URoom(SIEContext context) => _context = context;

        public Room SearchByCode(string code) => _context.Room.FirstOrDefault(r => string.Equals(r.Code, code, StringComparison.CurrentCultureIgnoreCase));

        public List<Room> GetByOwner(int idOwner) => _context.Room.Where(r => r.PersonId == idOwner).ToList();
    }
}
