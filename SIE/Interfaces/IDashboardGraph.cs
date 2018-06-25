using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIE.Models;

namespace SIE.Interfaces
{
    interface IDashboardGraph<T>
    {
        MDashboard CreateGraph(List<T> list);
    }
}
