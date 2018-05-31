using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Email{ get; set; }
        [ForeignKey("Institution")]
        public int? InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Sex { get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public int Profile { get; set; } 
    }
}
