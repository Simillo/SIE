using System;
using SIE.Context;

namespace SIE.Business
{
    public class BHistory
    {
        private readonly SIEContext _context;

        public BHistory(SIEContext context) => _context = context;

        public void SaveHistory(int personId, string action)
        {
            var history = new History
            {
                PersonId = personId,
                Action = action,
                DateAction = DateTime.Now
            };
            _context.History.Add(history);
            _context.SaveChanges();
        }
    }
}
