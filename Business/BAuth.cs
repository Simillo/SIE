using Microsoft.AspNetCore.Http;
using SIE.Context;

namespace SIE.Business
{
    public static class BAuth
    {
        public static void Authenticate(this ISession session, Person person)
        {
            session.SetString("_id", person.Id.ToString());
            session.SetString("_name", person.Name);
            session.SetString("_cpf", person.Cpf);
            session.SetString("_email", person.Email);
        }
    }
}
