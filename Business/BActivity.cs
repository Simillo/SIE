using SIE.Context;
using SIE.Enums;
using SIE.Models;

namespace SIE.Business
{
    public class BActivity
    {
        private readonly SIEContext _context;

        public BActivity(SIEContext context) => _context = context;

        public void SaveOrUpdate(MNewActivity newActivity, Room room)
        {
            var activity = new Activity
            {
                Id = newActivity.Id,
                ExpirationDate = newActivity.ExpirationDate ?? room.ExpirationDate,
                Title = newActivity.Title,
                Description = newActivity.Description,
                PersonId = room.PersonId,
                RoomId = room.Id,
                CurrentState = (int) EActivityState.Building,
                Weight = newActivity.Weight
            };

            if (activity.Id > 0)
                Update(activity);
            else
                Save(activity);
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
