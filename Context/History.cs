using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public DateTime DateAction { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
