using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.Models
{
    public class PlayerStats: DeleteableEntity
    {
        public int YellowCard {  get; set; }
        public int RedCard { get; set; }
        public int Score { get; set; }
        public int Assits { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public int MatchResultId { get; set; }
        [ForeignKey("MatchResultId")]
        public MatchResult MatchResult { get; set; }
    }
}
