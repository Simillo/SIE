﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SIE.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        [StringLength(50)]
        public string Email{ get; set; }
        public string Institution { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public int Sex { get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
