using System.Collections.Generic;
using SIE.Models;

namespace SIE.Interfaces
{
    public interface IDashboard
    {
        List<string> labels { get; set; }
        List<MDashboardDatasets> datasets { get; set; }
    }
}
