using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Utils
{
    public class URelUploadActivity
    {
        private readonly SIEContext _context;
        public URelUploadActivity(SIEContext context) => _context = context;

        public List<RelUploadActivity> GetByActivity(int activityId) => _context.RelUploadActivity.Where(r => r.Activity.Id == activityId).ToList();
    }
}
