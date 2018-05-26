using System.Collections.Generic;

namespace SIE
{
    public interface IEmail
    {
        void SendEmail(string subject, string body, IEnumerable<string> destination);
    }
}
