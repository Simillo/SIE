using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoreLinq;
using SIE.Business;
using SIE.Context;
using SIE.Enums;
using SIE.Scheduler.Interfaces;
using SIE.Services;
using SIE.Utils;

namespace SIE.Scheduler.Scheduler.Tasks
{
    public class ActivitiesExpiringTask : IScheduledTask
    {
        public string Schedule => "* * * * *";
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public ActivitiesExpiringTask(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var serviceProvider = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceProvider.CreateScope())
            {
                var sevenDaysFromNow = DateTime.Now.AddDays(7);

                var context = scope.ServiceProvider.GetService<SIEContext>();
                var toExpire =
                    new UActivity(context)
                        .Get()
                        .Where(r => r.CurrentState == (int)EActivityState.InProgress && r.ExpirationDate != null && r.ExpirationDate <= sevenDaysFromNow)
                        .ToList();

                if (!toExpire.Any()) return;

                var uRelStudentRoom = new URelStudentRoom(context);
                var students = (IEnumerable<RelStudentRoom>)new List<RelStudentRoom>();

                foreach (var activity in toExpire)
                {
                    students = students
                        .Concat(uRelStudentRoom.GetByRoom(activity.Room.Id));
                }

                students = students.DistinctBy(s => s.Person.Id);
                var sEmail = new EmailService(_configuration);
                var uAnswer = new UAnswer(context);

                foreach (var student in students)
                {
                    var body = "<h3>Atividades a serem entregues na próxima semana</h3><br/>";
                    toExpire
                        .Where(a => a.Room.Id == student.Room.Id && uAnswer.GetByUser(a.Id, student.Person.Id) == null)
                        .OrderBy(a => a.ExpirationDate)
                        .ForEach(t => { body += $"<li><b><a href='http://localhost:8080/#/student/room/{t.Room.Code}/activity/{t.Id}'>{t.Title}</a></b>" +
                                                        $" para o dia <b>{t.ExpirationDate:dd/MM/yyyy}</b>.";});

                    sEmail.SendEmail("Suas atividades a serem entregues durante a próxima semana", body, new List<string> { student.Person.Email });
                }
            }
        }
    }
}
;