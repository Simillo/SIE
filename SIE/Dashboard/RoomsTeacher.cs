using System.Collections.Generic;
using System.Linq;
using SIE.Context;
using SIE.Enums;
using SIE.Interfaces;
using SIE.Models;

namespace SIE.Dashboard
{
    public class RoomsTeacher : IDashboardGraph<Room>
    {
        public MDashboard CreateGraph(List<Room> rooms)
        {
            var dashboard = new MDashboard();
            var roomsCount = new List<dynamic>();
            var roomsColor = new List<string>();

            if (rooms.Any(r => r.CurrentState == (int)ERoomState.Building))
            {
                dashboard.labels.Add("Salas em construção");
                roomsCount.Add(rooms.Count(r => r.CurrentState == (int)ERoomState.Building));
                roomsColor.Add("#139A43");
            }

            if (rooms.Any(r => r.CurrentState == (int)ERoomState.Open))
            {
                dashboard.labels.Add("Salas abertas");
                roomsCount.Add(rooms.Count(r => r.CurrentState == (int)ERoomState.Open));
                roomsColor.Add("#3454D1");
            }

            if (rooms.Any(r => r.CurrentState == (int)ERoomState.Closed))
            {
                dashboard.labels.Add("Salas finalizadas");
                roomsCount.Add(rooms.Count(r => r.CurrentState == (int)ERoomState.Closed));
                roomsColor.Add("#F2DC5D");
            }

            dashboard.datasets.Add(new MDashboardDatasets
            {
                backgroundColor = roomsColor,
                data = roomsCount
            });
            return dashboard;
        }
    }
}
