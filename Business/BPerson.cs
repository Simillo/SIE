using System;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Models;

namespace SIE.Business
{
    public class BPerson
    {
        private readonly SIEContext _context;
        public BPerson(SIEContext context)
        {
            _context = context;
        }
        public void SaveOrUpdate(MPerson person)
        {
            var cPerson = new Person
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.Cpf.RCpf(),
                Email = person.Email,
                Institution = person.Institution,
                BirthDate = person.BirthDate,
                Sex = person.Sex,
                Password = person.Password,
                Profile = person.Profile
            };

            if (person.Id > 0)
            {
                cPerson = _context.Person.Find(cPerson.Id);
                Update(cPerson);
            }
            else
            {
                Save(cPerson);
            }

            _context.SaveChanges();

        }

        private void Save(Person person)
        {
            _context.Person.Add(person);
        }

        private void Update(Person person)
        {
            _context.Person.Update(person);
        }
    }
}
