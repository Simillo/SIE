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
        public string Schedule => "0 0 * * *";

        private readonly UPasswordRecovery _uPasswordRecovery;
        private readonly BPasswordRecovery _bPasswordRecovery;
        private readonly BHistory _bHistory;
        public ExpireTokenTask(IServiceProvider serviceProvider)
        {
            var context = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider
                .GetService<SIEContext>();

            _uPasswordRecovery = new UPasswordRecovery(context);
            _bPasswordRecovery = new BPasswordRecovery(context);
            _bHistory = new BHistory(context);
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var expired = _uPasswordRecovery.Get().Where(r => r.Active && now > r.ExpirationDate);
            foreach (var passwordRecovery in expired)
            {
                passwordRecovery.Active = false;
                passwordRecovery.CancelationDate = now;
                _bPasswordRecovery.Update(passwordRecovery);
                _bHistory.SaveHistory(passwordRecovery.Person.Id, "Solicitação de recuperar senha do usuário foi cancelada pois expirou");
            }
        }
    }
}
