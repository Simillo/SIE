using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Business;
using SIE.Context;
using SIE.Enums;
using SIE.Helpers;
using SIE.Models;
using SIE.Services;
using SIE.Utils;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly BHistory _bHistory;
        private readonly BPerson _bPerson;
        private readonly UPerson _uPerson;
        private readonly SEmail _sEmail;
        public PersonController(SIEContext context, IConfiguration configuration)
        {
            _bHistory = new BHistory(context);
            _bPerson = new BPerson(context);
            _uPerson = new UPerson(context);
            _sEmail = new SEmail(configuration);
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

            var newPerson = _bPerson.SaveOrUpdate(person);
            _bHistory.SaveHistory(newPerson.Id, "Usuário registrou no sistema");

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
            var res = person.Profile == (int)EProfile.Teacher ? "/teacher" : "/student";

            _bHistory.SaveHistory(person.Id, "Usuário autenticou no sistema");

            return Ok(ResponseContent.Create(res, HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Saindo..."));
        }

        [HttpGet]
        [Route("Teste")]
        public IActionResult Teste()
        {
            _sEmail.SendEmail("teste subject", "teste body", new List<string>{""});
            return Ok();
        }
    }
}
