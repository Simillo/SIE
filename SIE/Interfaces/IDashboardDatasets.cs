using System.Collections.Generic;

namespace SIE.Interfaces
{
    public interface IDashboardDatasets
    {
        string label { get; set; }
        List<string> backgroundColor { get; set; }
        List<dynamic> data { get; set; }
    }
}
