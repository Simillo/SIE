using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using SIE.Context;
using SIE.Interfaces;
using SIE.Models;

namespace SIE.Dashboard
{
    public class DashboardStudentsXRoom : IDashboardGraph<RelStudentRoom>
    {
        public MDashboard CreateGraph(List<RelStudentRoom> relStudentRooms)
        {
            var dashboard = new MDashboard();
            dashboard.labels.Add("Sala");

            var rooms = relStudentRooms.Select(r => r.Room).DistinctBy(r => r.Id).ToList();

            var rnd = new Random();
            foreach (var room in rooms)
            {
                var bgColor = $"#{rnd.Next(0x1000000):X6}";
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
