using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Business;
using SIE.Context;
using SIE.Helpers;
using SIE.Models;
using SIE.Utils;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly BPerson _bPerson;
        private readonly UPerson _uPerson;
        public PersonController(SIEContext context)
        {
            _bPerson = new BPerson(context);
            _uPerson = new UPerson(context);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] MPerson person)
        {
            if (!person.ModelValid())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Campos inválidos!"));

            var errors = new List<MModelError>();
            person.ListErrors(_uPerson, ref errors);
            if (errors.Any())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, errors));

            _bPerson.SaveOrUpdate(person);
            return Ok(ResponseContent.Create(null, HttpStatusCode.Created, "Pessoa salva com sucesso!"));
        }

        [HttpGet("{cpf}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Get(string cpf)
        {
            if (!cpf.ValidCpf())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "CPF inválido!"));

            var res = _uPerson.GetByCpf(cpf.RCpf());
            if (res.Count > 0)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "CPF já está em uso!"));
            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, null));
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Login([FromBody] MLogin login)
        {
            var person = _bPerson.Login(login);
            if (person == null)
                return Ok(null);

            Authenticate(person);

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("Teste")]
        public IActionResult Teste()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("_id")))
            {
                return BadRequest("Not ok");
            }

            return Ok(new
            {
                Id = HttpContext.Session.GetString("_id"),
                Nome = HttpContext.Session.GetString("_name"),
                Cpf = HttpContext.Session.GetString("_cpf"),
                Email = HttpContext.Session.GetString("_email"),
            });
        }

        private void Authenticate(Person person)
        {
            HttpContext.Session.SetString("_id", person.Id.ToString());
            HttpContext.Session.SetString("_name", person.Name);
            HttpContext.Session.SetString("_cpf", person.Cpf);
            HttpContext.Session.SetString("_email", person.Email);
        }
    }
}
