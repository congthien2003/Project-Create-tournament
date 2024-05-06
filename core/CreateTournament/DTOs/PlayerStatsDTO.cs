namespace CreateTournament.DTOs
{
    public class PlayerStatsDTO
    {
        public int Id { get; set; }
        public int? YellowCard { get; set; }
        public int? RedCard { get; set; }
        public int? Score { get; set; }
        public int? Assits { get; set; }
        public int PlayerId { get; set; }
        public int MatchResultId { get; set; }
    }
}
