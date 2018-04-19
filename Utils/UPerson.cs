using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class UPerson
    {
        private readonly SIEContext _context;

        public UPerson(SIEContext context) => _context = context;

        public List<Person> GetByCpf(string cpf) => _context.Person.Where(p => p.Cpf == cpf).ToList();
        public List<Person> GetByEmail(string email) => _context.Person.Where(p => p.Email == email).ToList();
    }
}
