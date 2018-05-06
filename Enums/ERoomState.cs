using System.ComponentModel.DataAnnotations;

namespace SIE.Enums
{
    public enum ERoomState
    {
        [Display(Description = "Em construção")]
        Building = 1,

        [Display(Description = "Aberta")]
        Open = 2,

        [Display(Description = "Fechada")]
        Closed = 3
    }
}
