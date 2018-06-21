using System.Collections.Generic;
using SIE.Context;
using SIE.Utils;

namespace SIE.Business
{
    public class BRelUploadAnswer
    {
        private readonly SIEContext _context;

        public BRelUploadAnswer(SIEContext context) => _context = context;


        public void Save(List<Document> documents, Answer answer)
        {
            foreach (var document in documents)
            {
                Save(document, answer);
            }
        }
        private void Save(Document document, Answer answer)
        {
            var relUploadAnswer = new RelUploadAnswer()
            {
                Document = document,
                Answer = answer,
                Active = true
            };
            _context.RelUploadAnswer.Add(relUploadAnswer);
            _context.SaveChanges();
        }
    }
}
