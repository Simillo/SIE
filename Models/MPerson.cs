using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Emit;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Utils;

namespace SIE.Models
{
    public class MPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public MInstitution Institution { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        public string Password { get; set; }
        public int Profile { get; set; }
        public void ListErrors(UPerson uPerson, ref List<MModelError> errors)
        {
            if (!Email.ValidEmail())
            {
                errors.Add(new MModelError
                {
                    MessageError = "E-mail inválido!",
                    HasError = true,
                    Property = "Email"
                });
            }
            else if(uPerson.GetByEmail(Email).Count > 0)
            {
                errors.Add(new MModelError
                {
                    MessageError = "E-mail já está em uso!",
                    HasError = true,
                    Property = "Email"
                });
            }

            if (!Cpf.ValidCpf())
            {
                errors.Add(new MModelError
                {
                    MessageError = "CPF inválido!",
                    HasError = true,
                    Property = "Cpf"
                });
            }

            if (Password.Length < 6)
            {
                errors.Add(new MModelError
                {
                    MessageError = "A senha deve conter pelo menos 6 digitos!",
                    HasError = true,
                    Property = "Password"
                });
            }
        }
    }
}
