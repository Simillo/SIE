using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Utils
{
    public class UAnswer
    {
        private readonly SIEContext _context;
        public UAnswer(SIEContext context) => _context = context;

        public List<Answer> GetByActivity(int activityId) => _context.Answer.Where(a => a.ActivityId == activityId).ToList();
        public List<Answer> GetByActivity(List<int> activitiesIds, int personId) => _context.Answer.Where(a => activitiesIds.Contains(a.ActivityId) && a.PersonId == personId).ToList();
        public Answer GetByUser(int activityId, int personId) => _context.Answer.FirstOrDefault(a => a.ActivityId == activityId && a.PersonId == personId);
    }
}
