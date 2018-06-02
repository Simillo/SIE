using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SIE.Business;
using SIE.Context;
using SIE.Scheduler.Interfaces;
using SIE.Utils;

namespace SIE.Scheduler.Scheduler.Tasks
{
    public class ExpireTokenTask : IScheduledTask
    {
        public string Schedule => "* * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
        }
    }
}
