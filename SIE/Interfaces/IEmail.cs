using System.Collections.Generic;

namespace Interfaces.SIE
{
    public interface IEmail
    {
        void SendEmail(string subject, string body, IEnumerable<string> destination);
    }
}
