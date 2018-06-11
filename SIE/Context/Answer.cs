using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
        public DateTime? EvaluatedDate { get; set; }

        [Required]
        public string UserAnswer { get; set; }
        public string Feedback { get; set; }

        public double Grade { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }


    }
}
