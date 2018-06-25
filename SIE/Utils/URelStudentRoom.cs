using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Utils
{
    public class URelStudentRoom
    {
        private readonly SIEContext _context;
        public URelStudentRoom(SIEContext context) => _context = context;

        public List<int> GetRoomIdByPersonId(int personId) =>
            _context
                .RelStudentRoom
                .Where(r => r.Person.Id == personId && r.Active)
                .Select(p => p.Room.Id)
                .ToList();

        public List<RelStudentRoom> GetByRoom(int roomId) =>
            _context
                .RelStudentRoom
                .Where(r => r.Room.Id == roomId)
                .ToList();

        public List<RelStudentRoom> GetByRelByTeacher(int authenticatedUserId) => 
            _context
                .RelStudentRoom
                .Where(r => r.Room.Person.Id == authenticatedUserId)
                .ToList();
    }
}
