using System;
using System.ComponentModel.DataAnnotations;

namespace SIE.Context
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public DateTime? ExpirateDate { get; set; }
        public string Description { get; set; }
    }
}
