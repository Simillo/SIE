using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class RelUploadAnswer
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("Answer")]
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        [Required]
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
