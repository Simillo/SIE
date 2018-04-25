using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Auxiliary;
using SIE.Context;

namespace SIE.Utils
{
    public class UInstitution
    {
        private readonly SIEContext _context;

        public UInstitution(SIEContext context) => _context = context;

        public List<Institution> Get() => _context.Institution.ToList();
        public List<Institution> Get(string name) => _context.Institution.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();
    }
}
