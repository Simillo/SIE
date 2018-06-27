using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIE.Context;
using SIE.Controllers;
using SIE.Enums;
using SIE.Models;
using Xunit;

namespace SIE.Tests.Controllers
{
    public class PersonControllerTest
    {
        public PersonControllerTest()
        {
            InitContext();
        }

        private SIEContext _context;
        private IConfiguration _configuration;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<SIEContext>().UseInMemoryDatabase();
            _configuration = new ConfigurationBuilder().Build();
            _context = new SIEContext(builder.Options);

            var person = new Person
            {
                Id = 1,
                BirthDate = DateTime.Now,
                Cpf = "43252525259",
                Email = "simillonakai@gmail.com",
                Name = "Pessoa teste",
                Password = "619bf74a84a52a1cb50a025654076dceb92a911c8929d8a9aec158c35ae359db",
                Profile = (int) EProfile.Teacher,
                Sex = 1
            };
            _context.Person.Add(person);
            _context.SaveChanges();
        }

        [Fact]
        public void TestGetInfoByTokenNaoExistente()
        {
            var token = "Token não existente";
            var controller = new PersonController(_context, _configuration);
            var res = controller.GetInfoByToken(token) as ObjectResult;
            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.Unauthorized, res.StatusCode);
            Assert.Equal("Essa solicitação não existe!", ((MResponseContent)res.Value).message);
        }

        [Fact]
        public void TestGetInfoByTokenNaoAtiva()
        {
            var token = "Token expirado";

            var passwordRecovery = new PasswordRecovery
            {
                Person = _context.Person.Find(1),
                Active = false,
                RequestDate = DateTime.Now,
                ExpirationDate = DateTime.Now,
                CancelationDate = DateTime.Now,
                Token = token
            };
            _context.PasswordRecovery.Add(passwordRecovery);
            _context.SaveChanges();

            var controller = new PersonController(_context, _configuration);
            var res = controller.GetInfoByToken(token) as ObjectResult;
            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.Unauthorized, res.StatusCode);
            Assert.Equal("Essa solicitação já expirou!", ((MResponseContent)res.Value).message);
        }
    }
}
