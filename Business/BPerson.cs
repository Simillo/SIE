using SIE.Context;

namespace SIE.Business
{
    public class BPerson
    {
        private readonly SIEContext _context;
        public BPerson(SIEContext context)
        {
            _context = context;
        }
        public void SaveOrUpdate(Person person)
        {
            if (person.Id > 0)
            {
                person = _context.Person.Find(person.Id);
                Update(person);
            }
            else
            {
                Save(person);
            }

        }

        private void Save(Person person)
        {
            _context.Add(person);
        }

        private void Update(Person person)
        {
            _context.Update(person);
        }
    }
}
