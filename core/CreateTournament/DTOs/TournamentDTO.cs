using System.Collections.ObjectModel;

namespace CreateTournament.DTOs
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int QuantityTeam { get; set; }
        public string Location { get; set; }
        public int FormatTypeId { get; set; }
        public int SportTypeId { get; set; }
        public int UserId {  get; set; }
    }
}
