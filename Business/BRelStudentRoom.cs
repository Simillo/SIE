using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Business
{
    public class BRelStudentRoom
    {
        private readonly SIEContext _context;
        public BRelStudentRoom(SIEContext context) => _context = context;

        public void Save(int personId, int roomId)
        {
            var rel = new RelStudentRoom
            {
                PersonId = personId,
                RoomId = roomId,
                Active = true,
                JoinDate = DateTime.Now
            };
            _context.RelStudentRoom.Add(rel);
            _context.SaveChanges();
        }

        public void Exit(int personId, int roomId)
        {
            var rel = _context
                .RelStudentRoom
                .FirstOrDefault(r => r.RoomId == roomId && r.PersonId == personId && r.Active);

            if (rel == null)
                return;

            rel.ExitDate = DateTime.Now;
            rel.Active = false;
            _context.RelStudentRoom.Update(rel);
            _context.SaveChanges();
        }
    }
}
