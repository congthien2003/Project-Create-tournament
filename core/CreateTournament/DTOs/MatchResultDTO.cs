namespace CreateTournament.DTOs
{
    public class MatchResultDTO
    {
        public int Id { get; set; }
        public int ScoreT1 { get; set; }
        public int ScoreT2 { get; set; }
        public DateTime Finish {  get; set; }
        public int IdMatch { get; set; }
        public MatchDTO Match { get; set; }
    }
}
