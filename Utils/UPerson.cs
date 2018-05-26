using System.Collections.Generic;
using System.Linq;
using SIE.Auxiliary;
using SIE.Context;

namespace SIE.Utils
{
    public class UPerson
    {
        private readonly SIEContext _context;

        public UPerson(SIEContext context) => _context = context;

        public List<Person> GetByCpf(string cpf) => _context.Person.Where(p => p.Cpf == cpf.RCpf()).ToList();
        public Person GetByEmail(string email) => _context.Person.FirstOrDefault(p => p.Email == email);

        public Person GetById(int idPerson) => _context.Person.Find(idPerson);
    }
}
