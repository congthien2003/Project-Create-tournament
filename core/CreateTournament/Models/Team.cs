using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.Models
{
    public class Team: DeleteableEntity
    {
        public string Name { get; set; }
        public string? ImageTeam { get; set; }
        public int? Point { get; set; }
        public Collection<Player> Players { get; set; }
        public int TournamentId { get; set; }
        [ForeignKey("TournamentId")]
        public Tournament Tournament { get; set; }
    }
}
