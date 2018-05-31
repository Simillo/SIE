using SIE.Context;

namespace SIE.Models
{
    public class MViewInfoToken
    {
        public MViewInfoToken(PasswordRecovery passwordRecovery)
        {
            Token = passwordRecovery.Token;
            Email = passwordRecovery.Person.Email;
        }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
