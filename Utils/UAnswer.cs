using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Utils
{
    public class UAnswer
    {
        private readonly SIEContext _context;
        public UAnswer(SIEContext context) => _context = context;

        public Answer GetById(int id) => _context.Answer.Find(id);
        public List<Answer> GetByActivity(int activityId) => _context.Answer.Where(a => a.Activity.Id == activityId).ToList();
        public List<Answer> GetByActivity(List<int> activitiesIds, int personId) => _context.Answer.Where(a => activitiesIds.Contains(a.Activity.Id) && a.Person.Id == personId).ToList();
        public Answer GetByUser(int activityId, int personId) => _context.Answer.FirstOrDefault(a => a.Activity.Id == activityId && a.Person.Id == personId);
    }
}
