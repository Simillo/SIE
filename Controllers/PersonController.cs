using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SIE.Business;
using SIE.Context;
using SIE.Helpers;
using SIE.Models;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class PersonController : Controller
    {
        private readonly SIEContext _context;
        private readonly BPerson _bPerson;
        public PersonController(SIEContext context)
        {
            _context = context;
            _bPerson = new BPerson(_context);
        }
        // GET api/values
        [HttpGet]
        public List<Person> Get()
        {
            return _context.Person.ToList();
        }

        [HttpPost]
        public IActionResult Post([FromBody] MPerson person)
        {
            if (!person.ModelValid())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Campos inválidos!"));
            try
            {
                var cPerson = new Person
                {
                    Id = person.Id,
                    Name = person.Name,
                    Cpf = person.Cpf,
                    Email = person.Email,
                    Institution = person.Institution,
                    BirthDate = person.BirthDate,
                    Sex = person.Sex,
                    Password = person.Password,
                    Profile = person.Profile
                };
                _bPerson.SaveOrUpdate(cPerson);
            }
            catch (Exception)
            {
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Problema ao salvar novo usuário!"));
            }
            return Ok(ResponseContent.Create(null, HttpStatusCode.Created, "Pessoa salva com sucesso!"));
        }
    }
}
