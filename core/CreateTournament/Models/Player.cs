using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateTournament.Models
{
    public class Player: DeleteableEntity
    {
        public string Name { get; set; }
        public string? ImagePlayer { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public Collection<PlayerStats> PlayerStats { get; set; }

    }
}
