using System.Collections.Generic;
using System.Linq;
using SIE.Context;

namespace SIE.Utils
{
    public class URelUploadAnswer
    {
        private readonly SIEContext _context;
        public URelUploadAnswer(SIEContext context) => _context = context;

        public List<RelUploadAnswer> GetByAnswer(int answerId) => _context.RelUploadAnswer.Where(r => r.Answer.Id == answerId && r.Active).ToList();
    }
}
