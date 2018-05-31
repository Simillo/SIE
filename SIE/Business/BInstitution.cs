using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;
using SIE.Utils;

namespace SIE.Business
{
    public class BInstitution
    {
        private readonly SIEContext _context;
        public BInstitution(SIEContext context)
        {
            _context = context;
        }

        public Institution Save(string name)
        {
            var institution = new Institution
            {
                Id = 0,
                Name = name
            };
            _context.Institution.Add(institution);
            _context.SaveChanges();
            return institution;
        }
    }
}
