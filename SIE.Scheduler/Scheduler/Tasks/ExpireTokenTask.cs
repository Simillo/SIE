using System.Threading;
using System.Threading.Tasks;
using SIE.Scheduler.Interfaces;

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
