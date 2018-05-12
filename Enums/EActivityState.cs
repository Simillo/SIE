using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIE.Enums
{
    public enum EActivityState
    {
        [Display(Description = "Em construção")]
        Building = 1,
        [Display(Description = "Em andamento")]
        InProgress = 2,
        [Display(Description = "Encerrada")]
        Done = 3
    }
}
