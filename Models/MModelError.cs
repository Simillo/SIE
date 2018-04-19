using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIE.Models
{
    public class MModelError
    {
        public string Property { get; set; }
        public bool HasError { get; set; }
        public string MessageError { get; set; }
    }
}
