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
        private readonly SIEContext _context;
        private readonly IConfiguration _configuration;
        public PersonControllerTest()
        {
            var builder = new DbContextOptionsBuilder<SIEContext>().UseInMemoryDatabase();
            _configuration = new ConfigurationBuilder().Build();
            _context = new SIEContext(builder.Options);

            var person = new Person
            {
                BirthDate = DateTime.Now,
                Cpf = "09505752164",
                Email = "simillonakai@gmail.com",
                Name = "Pessoa teste",
                Password = "619bf74a84a52a1cb50a025654076dceb92a911c8929d8a9aec158c35ae359db",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };
            _context.Person.Add(person);
            _context.SaveChanges();
        }

        #region Save (teacher, saving)
        [Fact]
        public void TestSaveConsigoCadastrarUmProfessorSemInstituicao()
        {
            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Cpf = "43754264206",
                Email = "email@do.professor.com",
                Name = "Nome professor",
                Password = "123123a",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.OK, res.StatusCode);
            Assert.Equal("Cadastro realizado com sucesso!", ((MResponseContent)res.Value).message);
        }

        [Fact]
        public void TestSaveConsigoCadastrarUmProfessorComInstituicao()
        {
            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Institution = new MInstitution
                {
                    Name = "UFRA"
                },
                Cpf = "84733187491",
                Email = "email2@do.professor.com",
                Name = "Nome professor",
                Password = "123123a",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.OK, res.StatusCode);
            Assert.Equal("Cadastro realizado com sucesso!", ((MResponseContent)res.Value).message);
        }

        [Fact]
        public void TestSaveConsigoCadastrarUmProfessorComInstituicaoJaExistente()
        {
            var institution = new Institution
            {
                Name = "UFLA"
            };
            _context.Institution.Add(institution);
            _context.SaveChanges();

            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Institution = new MInstitution
                {
                    Name = "UFLA"
                },
                Cpf = "37551053654",
                Email = "email3@do.professor.com",
                Name = "Nome professor",
                Password = "123123a",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.OK, res.StatusCode);
            Assert.Equal("Cadastro realizado com sucesso!", ((MResponseContent)res.Value).message);
        }
        #endregion

        #region Save (person, errors)
        [Fact]
        public void TestSaveNaoConsigoCadastrarUmaPessoaComEmailExistente()
        {
            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Institution = null,
                Cpf = "02820214428",
                Email = "simillonakai@gmail.com",
                Name = "Nome professor",
                Password = "123123a",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal("Campo(s) inválido(s)!", ((MResponseContent)res.Value).message);
        }

        [Fact]
        public void TestSaveNaoConsigoCadastrarUmaPessoaComEmailInvalido()
        {
            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Institution = null,
                Cpf = "44268762060",
                Email = "e-mailMuitoInválido",
                Name = "Nome professor",
                Password = "123123a",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal("Campo(s) inválido(s)!", ((MResponseContent)res.Value).message);
        }

        [Fact]
        public void TestSaveNaoConsigoCadastrarUmaPessoaComCpfInvalido()
        {
            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Institution = null,
                Cpf = "123",
                Email = "email3@do.professor.com",
                Name = "Nome professor",
                Password = "123123a",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal("Campo(s) inválido(s)!", ((MResponseContent)res.Value).message);
        }
        [Fact]
        public void TestSaveNaoConsigoCadastrarUmaPessoaComSenhaInvalida()
        {
            var mPerson = new MPerson
            {
                BirthDate = DateTime.Now,
                Institution = null,
                Cpf = "52660410216",
                Email = "email4@do.professor.com",
                Name = "Nome professor",
                Password = "11",
                Profile = (int)EProfile.Teacher,
                Sex = (int)ESex.Male
            };

            var controller = new PersonController(_context, _configuration);
            var res = controller.Save(mPerson) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal("Campo(s) inválido(s)!", ((MResponseContent)res.Value).message);
        }
        #endregion

        #region GetInfoByToken (Errors)
        [Fact]
        public void TestGetInfoByTokenNaoExistente()
        {
            const string token = "Token não existente";

            var controller = new PersonController(_context, _configuration);
            var res = controller.GetInfoByToken(token) as ObjectResult;

            Assert.NotNull(res);
            Assert.Equal((int)HttpStatusCode.Unauthorized, res.StatusCode);
            Assert.Equal("Essa solicitação não existe!", ((MResponseContent)res.Value).message);
        }

        [Fact]
        public void TestGetInfoByTokenNaoAtiva()
        {
            const string token = "Token expirado";

            var passwordRecovery = new PasswordRecovery
            {
                Person = _context.Person.FirstOrDefault(),
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
        #endregion

    }
}
