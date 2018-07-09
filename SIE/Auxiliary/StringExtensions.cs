using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SIE.Auxiliary
{
    public static class StringExtensions
    {
        public static string RCpf(this string cpf) => cpf?.Trim().Replace(".", "").Replace("-", "");

        public static bool ValidCpf(this string cpf)
        {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.RCpf();

            if (cpf.Length != 11)
                return false;

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }

        public static bool IsValidPassord(this string password) => 
            password.Length >= 6 &&
            Regex.Matches(password, @"[a-z]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Count > 0 &&
            Regex.Matches(password, @"[0-9]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Count > 0;

        public static bool IsValidEmail(this string email)
        {
            var e = new EmailAddressAttribute();
            return e.IsValid(email);
        }

        public static string Sha256Hash(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            var sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(s));

                foreach (var b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
