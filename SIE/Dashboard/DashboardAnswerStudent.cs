using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Enums;
using SIE.Interfaces;
using SIE.Models;
using SIE.Utils;

namespace SIE.Dashboard
{
    public class DashboardAnswerStudent : IDashboardGraph<Answer>
    {
        private readonly UActivity _uActivity;
        private readonly URelStudentRoom _uRelStudentRoom;
        private readonly Person _person;
        public DashboardAnswerStudent(SIEContext context, Person person)
        {
            _uActivity = new UActivity(context);
            _uRelStudentRoom = new URelStudentRoom(context);
            _person = person;
        }

        public MDashboard CreateGraph(List<Answer> answers)
        {
            var dashboard = new MDashboard();
            var answersCount = new List<dynamic>();
            var answersColor = new List<string>();

            var rooms = _uRelStudentRoom.GetRoomByPerson(_person.Id);
            if (rooms.Any())
            {
                var answeredAndDone = 0;
                var notAnsweredAndDone = 0;

                var answeredAndNotDone= 0;
                var notAnsweredAndNotDone = 0;
                foreach (var room in rooms)
                {
                    var activities = _uActivity.GetByRoom(room.Id).Where(a => a.CurrentState != (int) EActivityState.Building);
                    foreach (var activity in activities)
                    {
                        var alreadyAnswered = answers.FirstOrDefault(a => a.Activity.Id == activity.Id) != null;
                        var isDone = activity.CurrentState == (int) EActivityState.Done;

                        if (alreadyAnswered && isDone) ++answeredAndDone;
                        else if (!alreadyAnswered && isDone) ++notAnsweredAndDone;
                        else if (alreadyAnswered)++answeredAndNotDone;
                        else ++notAnsweredAndNotDone;
                        
                    }
                }

                if (answeredAndDone > 0)
                {
                    dashboard.labels.Add("Atividades respondidas e finalizadas");
                    answersColor.Add(ColorExtensions.RandomColorHex());
                    answersCount.Add(answeredAndDone);
                }


                if (notAnsweredAndDone > 0)
                {
                    dashboard.labels.Add("Atividades não respondidas e finalizadas");
                    answersColor.Add(ColorExtensions.RandomColorHex());
                    answersCount.Add(notAnsweredAndDone);
                }

                if (answeredAndNotDone > 0)
                {
                    dashboard.labels.Add("Atividades respondidas e não finalizadas");
                    answersColor.Add(ColorExtensions.RandomColorHex());
                    answersCount.Add(answeredAndNotDone);
                }


                if (notAnsweredAndNotDone > 0)
                {
                    dashboard.labels.Add("Atividades não respondidas e não finalizadas");
                    answersColor.Add(ColorExtensions.RandomColorHex());
                    answersCount.Add(notAnsweredAndNotDone);
                }



            }

            dashboard.datasets.Add(new MDashboardDatasets
            {
                backgroundColor = answersColor,
                data = answersCount
            });
            return dashboard;
        }
    }
}
