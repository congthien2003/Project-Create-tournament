using CreateTournament.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageTeam { get; set; }
        public int? Point { get; set; }
        public Collection<PlayerDTO> Players { get; set; }
        public int TournamentId { get; set; }
        public TournamentDTO TournamentDTO { get; set; }
    }
}
