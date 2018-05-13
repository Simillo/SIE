using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIE.Context
{
    public class RelStudentRoom
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId{ get; set; }
        public Room Room{ get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public bool Active { get; set; }
    }
}
