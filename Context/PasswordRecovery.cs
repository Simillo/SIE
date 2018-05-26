using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class PasswordRecovery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }

        public DateTime? RecoveryDate { get; set; }

        public DateTime? CancelationDate { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
