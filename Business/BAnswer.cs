using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SIE.Context;

namespace SIE.Business
{
    public class BAnswer
    {
        private readonly SIEContext _context;
        public BAnswer(SIEContext context) => _context = context;

        public void Save(int personId, int activityId, int roomId, string answer)
        {
            var newAnswer = new Answer
            {
                PersonId = personId,
                ActivityId = activityId,
                RoomId = roomId,
                UserAnswer = answer,
                SentDate = DateTime.Now
            };

            _context.Answer.Add(newAnswer);
            _context.SaveChanges();
        }
    }
}
