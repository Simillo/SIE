using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Context;

namespace SIE.Models
{
    public class MViewDocument
    {
        public MViewDocument(Document document)
        {
            Path = $"static/uploads/{document.FileName}";
        }

        public string Path { get; set; }
    }
}
