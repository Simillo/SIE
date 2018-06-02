using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIE.Context;
using SIE.Scheduler.Interfaces;
using SIE.Utils;

namespace SIE.Scheduler.Scheduler.Tasks
{
    public class ExpireTokenTask : IScheduledTask
    {
        public string Schedule => "* * * * *";
        private readonly UPasswordRecovery _uPasswordRecovery;
        public ExpireTokenTask(IServiceProvider serviceProvider)
        {
            var context = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider
                .GetService<SIEContext>();

            _uPasswordRecovery = new UPasswordRecovery(context);
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
        }
    }
}
