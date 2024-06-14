using System.Collections.ObjectModel;

namespace CreateTournament.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public int IdTeam1 { get; set; }
        public int IdTeam2 { get; set; }
        public int STT { get; set; }
        public int round {  get; set; }
        public DateTime Created {  get; set; }
        public DateTime StartAt { get; set; }
        public int TournamentId { get; set; }
    }
}
