using System.Collections.Generic;
using SIE.Interfaces;

namespace SIE.Models
{
    public class MDashboard : IDashboard
    {
        public List<string> labels { get; set; } = new List<string>();
        public List<MDashboardDatasets> datasets { get; set; } = new List<MDashboardDatasets>();
    }
}
