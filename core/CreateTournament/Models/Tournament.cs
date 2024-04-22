using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.Models
{
    public class Tournament : DeleteableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int QuantityTeam { get; set; }
        public string Location { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int FormatTypeId { get; set; }
        [ForeignKey("FormatTypeId")]
        public FormatType FormatType { get; set; }
        public int SportTypeId { get; set; }
        [ForeignKey("SportTypeId")]
        public SportType SportType { get; set; }
        public Collection<Team> Teams { get; set; }
    }
}
