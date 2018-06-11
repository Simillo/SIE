using System;
using System.Collections.Generic;
using SIE.Context;

namespace SIE.Business
{
    public class BDocument
    {
        private readonly SIEContext _context;
        public BDocument(SIEContext context) => _context = context;


        public List<Document> Save(List<string> filesName, Person person)
        {
            var documents = new List<Document>();
            foreach (var fileName in filesName)
            {
                documents.Add(Save(fileName, person));
            }

            return documents;
        }
        private Document Save(string fileName, Person person)
        {
            var document = new Document
            {
                FileName = fileName,
                Person = person,
                UploadDate = DateTime.Now
            };
            _context.Document.Add(document);
            _context.SaveChanges();
            return document;
        }
    }
}
