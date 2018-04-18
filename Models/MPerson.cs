using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string Institution { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Profile { get; set; }

        private bool PasswordMatch()
        {
            return Password == ConfirmPassword;
        }

        private bool ValidEmail()
        {
            var email = new EmailAddressAttribute();
            return email.IsValid(Email);
        }

        public bool ModelValid()
        {
            return Cpf.ValidCpf() && ValidEmail() && PasswordMatch();
        }

        public List<string> Exists(UPerson uPerson)
        {
            var cpfInUse = uPerson.GetByCpf(Cpf.RCpf()).Count > 0;
            var emailInUse = uPerson.GetByCpf(Email).Count > 0;
            var result = new List<string>();
            if (cpfInUse)
                result.Add("CPF já está em uso!");
            if (emailInUse)
                result.Add("E-mail já está em uso!");
            return result;
        }
    }
}
