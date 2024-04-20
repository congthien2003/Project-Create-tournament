using System.Collections.ObjectModel;

namespace CreateTournament.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public int IdTeam1 { get; set; }
        public int IdTeam2 { get; set; }
        public DateTime Created {  get; set; }
        public DateTime StartAt { get; set; }
        public int TouramentId { get; set; }
        public TouramentDTO Tourament { get; set; }
        public Collection<TeamDTO> Teams { get; set; }
    }
}
