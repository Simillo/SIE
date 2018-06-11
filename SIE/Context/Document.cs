using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class Document
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public string FileName { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }
    }
}
