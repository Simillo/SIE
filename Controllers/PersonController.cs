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
        private readonly URecoveryPassword _uRecoveryPassword;
        public PersonController(SIEContext context, IConfiguration configuration)
        {
            _bHistory = new BHistory(context);
            _bPerson = new BPerson(context);
            _bPasswordRecovery = new BPasswordRecovery(context, configuration);
            _uPerson = new UPerson(context);
            _uRecoveryPassword = new URecoveryPassword(context);
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

            var newPerson = _bPerson.Save(person);
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
        [Route("GetInfoByToken/{token}")]
        public IActionResult GetInfoByToken(string token)
        {
            if (HttpContext.Session.IsAuth())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você já está autenticado!"));

            var info = _uRecoveryPassword.GetByToken(token);

            if (info == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Essa solicitação não existe!"));

            if(DateTime.Now > info.ExpirationDate || !info.Active)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Essa solicitação já expirou!"));

            return Ok(ResponseContent.Create(new MViewInfoToken(info), HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("Recovery/{email}")]
        public IActionResult Recovery(string email)
        {
            if (HttpContext.Session.IsAuth())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você já está autenticado!"));

            var person = _uPerson.GetByEmail(email);
            if (person == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "E-mail não cadastrado no sistema!"));

            _bPasswordRecovery.Request(person);
            _bHistory.SaveHistory(person.Id, "Usuário solicitou a recuperação da senha");
            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Solicitação enviada com sucesso, verifique sua caixa de entrada!"));
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] MUpdatePassword updatePassword)
        {
            if (updatePassword.Password.Length < 6)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A senha deve conter ao menos 6 caracteres!"));

            var passwordRecovery = _uRecoveryPassword.GetByToken(updatePassword.Token);

            if (passwordRecovery == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Essa solicitação não existe!"));

            if (DateTime.Now > passwordRecovery.ExpirationDate || !passwordRecovery.Active)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Essa solicitação já expirou!"));

            var newPerson = passwordRecovery.Person;
            newPerson.Password = updatePassword.Password.Sha256Hash();
            _bPerson.Update(newPerson);

            _bPasswordRecovery.Recovery(passwordRecovery);

            _bHistory.SaveHistory(newPerson.Id, "Usuário alterou a senha através da recuperação de senhas");
            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Senha alterada com sucesso!"));
        }
    }
}
