using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.Models
{
    public class Match : DeleteableEntity
    {
        public int IdTeam1 { get; set; }
        public int IdTeam2 { get; set; }
        public DateTime Created {  get; set; } = DateTime.UtcNow ;
        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public int round { get; set; }
        public int TournamentId { get; set; }
        [ForeignKey("TournamentId")]
        public Tournament Tournament { get; set; }
    }
}
