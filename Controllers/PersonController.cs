using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

        [HttpGet("{text}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Get(string text)
        {
            var res = new List<Person>();
            var rgx = new Regex(@"[^\d]", RegexOptions.IgnoreCase);

            if (rgx.Match(text).Length > 0)
            {
                if (!text.ValidEmail())
                    return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "E-mail inválido!"));
                res = _uPerson.GetByEmail(text);

                if (res.Any())
                    return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "E-mail já está em uso!"));

                return Ok(ResponseContent.Create(null, HttpStatusCode.OK, null));

            }
            if (!text.ValidCpf())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "CPF inválido!"));

            res = _uPerson.GetByCpf(text.RCpf());
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
            var person = _bPerson.SearchForPerson(login);
            if (person == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "E-mail ou senha incorreto(s)!"));

            HttpContext.Session.Authenticate(person);

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, null));
        }
    }
}
