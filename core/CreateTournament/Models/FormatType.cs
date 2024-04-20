using System.ComponentModel.DataAnnotations;

namespace CreateTournament.Models
{
    public class FormatType : DeleteableEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
