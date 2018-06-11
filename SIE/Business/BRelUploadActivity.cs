using System.Collections.Generic;
using SIE.Context;

namespace SIE.Business
{
    public class BRelUploadActivity
    {
        private readonly SIEContext _context;
        public BRelUploadActivity(SIEContext context) => _context = context;


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
    }
}
