using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SIE.Context;


namespace SIE.Auxiliary
{
    public static class SessionExtensions
    {
        public static dynamic GetCurrentPerson(this ISession session)
        {
            return new
            {
                Name = session.GetString("_name"),
                Email = session.GetString("_email"),
                Cpf = session.GetString("_cpf")
            };
        }

        public static bool IsAuth(this ISession session) => session.GetInt32("_id") > 0;

        public static void Authenticate(this ISession session, Person person)
        {
            session.SetInt32("_id", person.Id);
            session.SetString("_name", person.Name);
            session.SetString("_cpf", person.Cpf);
            session.SetString("_email", person.Email);
            session.SetInt32("_profile", person.Profile);
        }

        public static int GetSessionPersonId(this ISession session) => session.GetInt32("_id") ?? 0;
    }
}
