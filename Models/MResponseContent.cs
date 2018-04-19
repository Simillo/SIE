using System.Net;

namespace SIE.Models
{
    public class MResponseContent
    {
        public object entity { get; set; }
        public HttpStatusCode status { get; set; }
        public object message { get; set; }
    }
}
