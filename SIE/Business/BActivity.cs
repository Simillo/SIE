using System;
using System.Collections.Generic;
using SIE.Context;
using SIE.Enums;
using SIE.Models;
using SIE.Utils;

namespace SIE.Business
{
    public class BActivity
    {
        private readonly SIEContext _context;
        private readonly UActivity _uActivity;

        public BActivity(SIEContext context)
        {
            _context = context;
            _uActivity = new UActivity(context);
        }

        public void SaveOrUpdate(Activity activity)
        {
            if (activity.Id > 0)
                Update(activity);
            else
                Save(activity);
        }

        public Activity SaveOrUpdate(MNewActivity mActivity, Room room)
        {
            var activity = new Activity();
            if (mActivity.Id > 0)
                activity = _uActivity.GetById(mActivity.Id);

            activity.ExpirationDate = mActivity.ExpirationDate ?? room.ExpirationDate;
            activity.Title = mActivity.Title;
            activity.Description = mActivity.Description;
            activity.PersonId = room.PersonId;
            activity.RoomId = room.Id;
            activity.CurrentState = (int) EActivityState.Building;
            activity.Weight = mActivity.Weight;

            if (activity.Id > 0)
                Update(activity);
            else
                Save(activity);
            return activity;
        }

        public void CloseAll(List<Activity> activities)
        {
            foreach (var activity in activities)
            {
                activity.CurrentState = (int) EActivityState.Done;
                activity.EndDate = DateTime.Now;
                Update(activity);
            }
        }

        private void Update(Activity activity)
        {
            _context.Activity.Update(activity);
            _context.SaveChanges();
        }

        private void Save(Activity activity)
        {
            _context.Activity.Add(activity);
            _context.SaveChanges();
        }
    }
}
