using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SIE.Context;
using SIE.Services;
using SIE.Utils;

namespace SIE.Business
{
    public class BPasswordRecovery
    {
        private readonly SIEContext _context;
        private readonly UPasswordRecovery _uPasswordRecovery;
        private readonly EmailService _sEmail;

        public BPasswordRecovery(SIEContext context, IConfiguration configuration)
        {
            _context = context;
            _uPasswordRecovery = new UPasswordRecovery(context);
            _sEmail = new EmailService(configuration);
        }

        public void Request(Person person, ref bool hasOpenRequest)
        {
            var currentActive = _uPasswordRecovery.GetUserCurrentActive(person.Id);
            if (currentActive != null)
            {
                currentActive.Active = false;
                currentActive.CancelationDate = DateTime.Now;
                _context.PasswordRecovery.Update(currentActive);
                hasOpenRequest = true;
            }

            var passwordRecovery = new PasswordRecovery
            {
                Person = person,
                RequestDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(1),
                Active = true,
                Token = Guid.NewGuid().ToString()
            };
            _context.PasswordRecovery.Add(passwordRecovery);
            _context.SaveChanges();

            SendEmail(passwordRecovery);
        }

        public void Update(PasswordRecovery passwordRecovery)
        {
            _context.PasswordRecovery.Update(passwordRecovery);
            _context.SaveChanges();
        }

        private void SendEmail(PasswordRecovery passwordRecovery)
        {
            var body = $"<h1>Olá, {passwordRecovery.Person.Name}</h1>" +
                       $"<br/>Clique <a href='http://localhost:8080/#/password-recovery/{passwordRecovery.Token}/'>aqui</a>" +
                       " para cadastrar uma nova senha.<br/>" +
                       $"http://localhost:8080/#/password-recovery/{passwordRecovery.Token}/";

            _sEmail.SendEmail("Recuperação de senha", body, new List<string> { passwordRecovery.Person.Email });
        }
    }
}
