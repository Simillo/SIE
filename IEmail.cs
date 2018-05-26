using System.Collections.Generic;

namespace SIE
{
    interface IEmail
    {
        void SendEmail(string subject, string body, IEnumerable<string> destination);
    }
}
