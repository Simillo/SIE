﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace SIE.Services
{
    public abstract class SEmail : IEmail
    {
        private readonly IConfiguration _configuration;
        protected SEmail(IConfiguration configuration) => _configuration = configuration;
        public void SendEmail(string subject, string body, IEnumerable<string> destination)
        {
            var client = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:Username"]),
                Body = body,
                IsBodyHtml = true,
                Subject = subject
            };
            foreach (var email in destination)
            {
                mailMessage.To.Add(email);
            }
            client.Send(mailMessage);
        }
    }
}