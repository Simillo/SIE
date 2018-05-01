using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Business;
using SIE.Context;
using SIE.Enums;
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
        [Route("Save")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Save([FromBody] MPerson person)
        {
            if (person == null) 
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Existe(m) campo(s) obrigatório(s) vazio(s)!"));

            var errors = new List<MModelError>();
            person.ListErrors(_uPerson, ref errors);

            if (errors.Any())
                return BadRequest(ResponseContent.Create(errors, HttpStatusCode.BadRequest, "Campo(s) inválido(s)!"));

            _bPerson.SaveOrUpdate(person);
            return Ok(ResponseContent.Create(null, HttpStatusCode.Created, "Cadastro realizado com sucesso!"));
        }


        [HttpPost]
        [Route("Login")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Login([FromBody] MLogin login)
        {
            var person = _bPerson.SearchForPerson(login);
            if (person == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "E-mail e/ou senha incorreto(s)!"));

            HttpContext.Session.Authenticate(person);
            var res = HttpContext.Session.GetInt32("_profile") == (int)EProfile.Teacher ? "/teacher" : "/student";

            return Ok(ResponseContent.Create(res, HttpStatusCode.OK, null));
        }
    }
}
