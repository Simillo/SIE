using System.Collections.Generic;
using System.Linq;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Interfaces;
using SIE.Models;

namespace SIE.Dashboard
{
    public class DashboardStudentsXRoom : IDashboardGraph<Room>
    {
        public MDashboard CreateGraph(List<Room> rooms)
        {
            var dashboard = new MDashboard();
            if (!rooms.Any()) return dashboard;

            dashboard.labels.Add("Sala");

            foreach (var room in rooms)
            {
                var bgColor = ColorExtensions.RandomColorHex();
                var dataset = new MDashboardDatasets
                {
                    label = room.Name,
                    backgroundColor = new List<string> {bgColor},
                    data = new List<dynamic> {room.NumberOfStudents}
                };

                dashboard.datasets.Add(dataset);
            }
            return dashboard;
        }
    }
}
