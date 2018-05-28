﻿using System;
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
        private readonly URecoveryPassword _uRecoveryPassword;
        private readonly EmailService _sEmail;
        public BPasswordRecovery(SIEContext context, IConfiguration configuration)
        {
            _context = context;
            _uRecoveryPassword = new URecoveryPassword(context);
            _sEmail = new EmailService(configuration);
        }

        public void Request(Person person)
        {
            var currentActive = _uRecoveryPassword.GetUserCurrentActive(person.Id);
            if (currentActive != null)
            {
                currentActive.Active = false;
                currentActive.CancelationDate = DateTime.Now;
                _context.PasswordRecovery.Update(currentActive);
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

        public void Recovery(PasswordRecovery passwordRecovery)
        {
            passwordRecovery.Active = false;
            passwordRecovery.RecoveryDate = DateTime.Now;
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
