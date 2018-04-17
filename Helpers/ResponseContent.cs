using System.Net;
using SIE.Models;

namespace SIE.Helpers
{
    public class ResponseContent
    {
        public static MResponseContent Create(object entity, HttpStatusCode status, string message)
        {
            return new MResponseContent
            {
                entity = entity,
                status = status,
                message = message
            };
        }
    }
}
