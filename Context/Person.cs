using System;
using System.ComponentModel.DataAnnotations;
using SIE.Models;

namespace SIE.Context
{
    public class Person
    {
        public Person(){}

        public Person(MPerson person)
        {
            Id = person.Id;
            Name = person.Name;
            Cpf = person.Cpf;
            Email = person.Email;
            Institution = person.Institution;
            BirthDate = person.BirthDate;
            Sex = person.Sex;
            Password = person.Password;
            Profile = person.Profile;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Email{ get; set; }
        public string Institution { get; set; }
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
