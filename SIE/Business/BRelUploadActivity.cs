using System.Collections.Generic;
using SIE.Context;
using SIE.Utils;

namespace SIE.Business
{
    public class BRelUploadActivity
    {
        private readonly SIEContext _context;

        private readonly URelUploadActivity _uRelUploadActivity;
        public BRelUploadActivity(SIEContext context)
        {
            _context = context;
            _uRelUploadActivity = new URelUploadActivity(context);
        }


        public void Save(List<Document> documents, Activity activity)
        {
            foreach (var document in documents)
            {
                Save(document, activity);
            }
        }
        private void Save(Document document, Activity activity)
        {
            var relUploadActivity = new RelUploadActivity
            {
                Document = document,
                Activity = activity,
                Active = true
            };
            _context.RelUploadActivity.Add(relUploadActivity);
            _context.SaveChanges();
        }

        private void Update(RelUploadActivity relUploadActivity)
        {
            _context.RelUploadActivity.Update(relUploadActivity);
            _context.SaveChanges();
        }

        public void DeactivateByActivity(int activityId)
        {
            var relUploads = _uRelUploadActivity.GetByActivity(activityId);
            foreach (var relUpload in relUploads)
            {
                relUpload.Active = false;
                Update(relUpload);
            }
        }
    }
}
