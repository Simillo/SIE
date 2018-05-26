using System;
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
        private readonly BPasswordRecovery _bPasswordRecovery;
        private readonly UPerson _uPerson;
        private readonly SEmail _sEmail;
        public PersonController(SIEContext context, IConfiguration configuration)
        {
            _bHistory = new BHistory(context);
            _bPerson = new BPerson(context);
            _bPasswordRecovery = new BPasswordRecovery(context);
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
        [Route("Recovery/{email}")]
        public IActionResult Recovery(string email)
        {
            if (HttpContext.Session.IsAuth())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você já está autenticado!"));

            var person = _uPerson.GetByEmail(email).FirstOrDefault();
            if (person == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "E-mail não cadastrado no sistema!"));

            var passwordRecovery = new PasswordRecovery
            {
                Person = person
            };
            _bPasswordRecovery.Save(ref passwordRecovery);

            _sEmail.SendEmail("Recuperação de senha", $"<h1>Olá, {person.Name}</h1><br/><a href='http://localhost:8080/#/password-recovery/{passwordRecovery.Token}/'>Clique aqui para cadastrar uma nova senha.</a><br/>http://localhost:8080/#/password-recovery/{passwordRecovery.Token}/", new List<string> { person.Email });
            return Ok();
        }
    }
}
