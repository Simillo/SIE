using System.Collections.Generic;
using SIE.Interfaces;

namespace SIE.Models
{
    public class MDashboardDatasets : IDashboardDatasets
    {
        public string label { get; set; }
        public List<string> backgroundColor { get; set; } = new List<string>();
        public List<dynamic> data { get; set; } = new List<dynamic>();
    }
}
