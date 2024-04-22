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
        public int IdFormatType { get; set; }
        public FormatTypeDTO FormatType { get; set; }
        public int IdSportType { get; set; }
        public SportTypeDTO SportType { get; set; }
        public int IdUser {  get; set; }
        public UserDTO User { get; set; }
        public Collection<TeamDTO> Teams { get; set; }
    }
}
