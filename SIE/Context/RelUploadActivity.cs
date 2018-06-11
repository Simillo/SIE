using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class RelUploadActivity
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [Required]
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
