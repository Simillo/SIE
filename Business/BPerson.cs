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
        private readonly UInstitution _uInstitution;
        private readonly BInstitution _bInstitution;
        public BPerson(SIEContext context)
        {
            _context = context;
            _uPerson = new UPerson(_context);
            _uInstitution = new UInstitution(_context);
            _bInstitution = new BInstitution(_context);
        }
        public Person SaveOrUpdate(MPerson person)
        {
            var institutionId = 0;
            if (!string.IsNullOrEmpty(person.Institution?.Name))
            {
                institutionId = _uInstitution.GetExact(person.Institution.Name)?.Id ?? 0;
                if (institutionId == 0)
                    institutionId = _bInstitution.Save(person.Institution.Name).Id;
            }
            var cPerson = new Person
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.Cpf.RCpf(),
                Email = person.Email,
                InstitutionId = institutionId,
                BirthDate = person.BirthDate,
                Sex = person.Sex,
                Password = person.Password.Sha256Hash(),
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
            return cPerson;

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

            var person = _uPerson.GetByEmail(login.Email);
            if (person != null && person.Password == login.Password.Sha256Hash())
                return person;

            return null;
        }
    }
}
