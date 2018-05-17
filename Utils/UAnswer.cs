using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class UAnswer
    {
        private readonly SIEContext _context;
        public UAnswer(SIEContext context) => _context = context;

        public List<Answer> GetByActivity(int activityId) => _context.Answer.Where(a => a.ActivityId == activityId).ToList();

        public Answer GetByUser(int activityId, int personId) => _context.Answer.FirstOrDefault(a => a.ActivityId == activityId && a.PersonId == personId);
    }
}
