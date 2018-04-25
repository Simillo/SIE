using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Models;
using SIE.Utils;

namespace SIE.Business
{
    public class BPerson
    {
        private readonly SIEContext _context;
        private readonly UPerson _uPerson;
        public BPerson(SIEContext context)
        {
            _context = context;
            _uPerson = new UPerson(_context);
        }
        public void SaveOrUpdate(MPerson person)
        {
            var cPerson = new Person
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.Cpf.RCpf(),
                Email = person.Email,
                InstitutionId = person.Institution.Id,
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

        public Person SearchForPerson(MLogin login)
        {
            if (!login.Email.ValidEmail()) return null;

            var listResult = _uPerson.GetByEmail(login.Email);
            return listResult.FirstOrDefault(p => p.Password == login.Password);

        }
    }
}
