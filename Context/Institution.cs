
using System.ComponentModel.DataAnnotations;
using SIE.Models;

namespace SIE.Context
{
    public class Institution
    {
        public Institution(){}

        public Institution(MInstitution institution)
        {
            Id = institution.Id;
            Name = institution.Name;
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
