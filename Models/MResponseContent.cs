using System.Net;

namespace SIE.Models
{
    public class MResponseContent
    {
        public object entity { get; set; }
        public string message { get; set; }
        public HttpStatusCode status { get; set; }
    }
}
