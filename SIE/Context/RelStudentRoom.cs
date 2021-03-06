﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIE.Context
{
    public class RelStudentRoom
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public bool Active { get; set; }
    }
}
