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
        public BRoom(SIEContext context) => _context = context;

        public void SaveOrUpdate(Room room)
        {
            if (room.Id > 0)
                Update(room);
            else
                Save(room);
        }

        public void Save(MNewRoom newRoom, int personId)
        {
            var room = new Room(newRoom, personId);
            Save(room);
        }

        private void Save(Room room)
        {
            _context.Room.Add(room);
            _context.SaveChanges();
        }

        private void Update(Room room)
        {
            _context.Room.Update(room);
            _context.SaveChanges();
        }
    }
}
