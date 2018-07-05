using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class UActivity
    {
        private readonly SIEContext _context;

        public UActivity(SIEContext context) => _context = context;

        public List<Activity> Get() => _context.Activity.ToList();
        public Activity GetById(int id) => _context.Activity.Find(id);
        public List<Activity> GetByUser(int personId) => _context.Activity.Where(a => a.Person.Id == personId).ToList();
        public List<Activity> GetByRoom(int roomId) => _context.Activity.Where(a => a.Room.Id == roomId).ToList();
        public List<Activity> GetByRoomAndUser(int roomId, int personId) => _context.Activity.Where(a => a.Room.Id == roomId && a.Person.Id == personId).ToList();
    }
}
