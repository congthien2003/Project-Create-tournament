using System.Collections.ObjectModel;

namespace CreateTournament.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageTeam { get; set; }
        public Collection<PlayerDTO> Players { get; set; }
    }
}
