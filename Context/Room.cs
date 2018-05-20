using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIE.Enums;
using SIE.Models;

namespace SIE.Context
{
    public class Room
    {
        public Room(){}

        public Room(MNewRoom newRoom, int personId)
        {
            Id = 0;
            Name = newRoom.Name;  
            Code = newRoom.Code;
            ExpirationDate = newRoom.ExpirationDate;
            Description = newRoom.Description;
            CurrentState = (int) ERoomState.Building;
            NumberOfStudents = 0;
            PersonId = personId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set ;}

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string Description { get; set; }
        public int NumberOfStudents { get; set; }
        public int CurrentState { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
