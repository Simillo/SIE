using System.ComponentModel.DataAnnotations;

namespace SIE.Context
{
    public class Institution
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
