using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SIE.Business;
using SIE.Context;
using SIE.Scheduler.Interfaces;
using SIE.Utils;

namespace SIE.Scheduler.Scheduler.Tasks
{
    public class ExpireTokenTask : IScheduledTask
    {
        public string Schedule => "* * * * *";
        private readonly IServiceProvider _serviceProvider;
        public ExpireTokenTask(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var serviceProvider = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceProvider.CreateScope())
            {
                var now = DateTime.Now;

                var context = scope.ServiceProvider.GetService<SIEContext>();
                var expired = 
                    new UPasswordRecovery(context)
                    .Get()
                    .Where(r => r.Active && now > r.ExpirationDate)
                    .ToList();

                if (!expired.Any()) return;

                var bPasswordRecovery = new BPasswordRecovery(context);
                var bHistory = new BHistory(context);
                foreach (var passwordRecovery in expired)
                {
                    passwordRecovery.Active = false;
                    passwordRecovery.CancelationDate = now;
                    bPasswordRecovery.Update(passwordRecovery);
                    bHistory.SaveHistory(passwordRecovery.Person.Id, "Solicitação de recuperar senha do usuário foi cancelada pois expirou");
                }
            }
        }
    }
}
