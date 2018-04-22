using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
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

            HttpContext.Session.Authenticate(person);

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("Teste")]
        public IActionResult Teste()
        {
            if (HttpContext.Session.IsAuth())
            {
                return Ok("ok");
            }

            return BadRequest("not ok");
        }
    }
}
