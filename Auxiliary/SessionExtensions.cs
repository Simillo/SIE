using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SIE.Context;


namespace SIE.Auxiliary
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static bool IsAuth(this ISession session) => !string.IsNullOrEmpty(session.GetString("_id"));

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
