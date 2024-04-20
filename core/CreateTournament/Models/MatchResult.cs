using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.Models
{
    public class MatchResult: DeleteableEntity
    {
        public int ScoreT1 { get; set; }
        public int ScoreT2 { get; set; }
        public int IdTeamWin { get; set; }
        public DateTime Finish { get; set; }
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        public Match Match { get; set; }
        public Collection<PlayerStats> PlayerStats { get; set; }
    }
}
