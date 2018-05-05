using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;
using SIE.Enums;
using SIE.Models;

namespace SIE.Business
{
    public class BRoom
    {
        private readonly SIEContext _context;
        public BRoom(SIEContext context)
        {
            _context = context;
        }
        public void Save(MNewRoom newRoom, int personId)
        {
            var room = new Room(newRoom, personId);
            _context.Room.Add(room);
            _context.SaveChanges();
        }
    }
}
