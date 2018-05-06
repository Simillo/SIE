using SIE.Context;

namespace SIE.Business
{
    public class BHistory
    {
        private readonly SIEContext _context;

        public BHistory(SIEContext context) => _context = context;

        public void SaveHistory(History history)
        {
            _context.History.Add(history);
            _context.SaveChanges();
        }
    }
}
