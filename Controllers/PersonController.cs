using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIE.Context;
using SIE.Helpers;
using SIE.Models;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly SIEContext _context;
        public PersonController(SIEContext context)
        {
            _context = context;
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
            return Ok(ResponseContent.Create(null, HttpStatusCode.Created, "Pessoa salva com sucesso!"));
        }
    }
}
