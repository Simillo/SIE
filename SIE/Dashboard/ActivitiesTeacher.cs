using System.Collections.Generic;
using System.Linq;
using SIE.Context;
using SIE.Enums;
using SIE.Interfaces;
using SIE.Models;

namespace SIE.Dashboard
{
    public class ActivitiesTeacher : IDashboardGraph<Activity>
    {
        public MDashboard CreateGraph(List<Activity> activities)
        {
            var dashboard = new MDashboard();
            var acitiviesCount = new List<dynamic>();
            var acitivitiesColor = new List<string>();

            if (activities.Any(r => r.CurrentState == (int)EActivityState.Building))
            {
                dashboard.labels.Add("Atividades em construção");
                acitiviesCount.Add(activities.Count(r => r.CurrentState == (int)EActivityState.Building));
                acitivitiesColor.Add("#139A43");
            }

            if (activities.Any(r => r.CurrentState == (int)EActivityState.InProgress))
            {
                dashboard.labels.Add("Atividades em andamento");
                acitiviesCount.Add(activities.Count(r => r.CurrentState == (int)EActivityState.InProgress));
                acitivitiesColor.Add("#3454D1");
            }

            if (activities.Any(r => r.CurrentState == (int)EActivityState.Done))
            {
                dashboard.labels.Add("Atividades finalizadas");
                acitiviesCount.Add(activities.Count(r => r.CurrentState == (int)EActivityState.Done));
                acitivitiesColor.Add("#F2DC5D");
            }

            dashboard.datasets.Add(new MDashboardDatasets
            {
                backgroundColor = acitivitiesColor,
                data = acitiviesCount
            });
            return dashboard;
        }
    }
}
