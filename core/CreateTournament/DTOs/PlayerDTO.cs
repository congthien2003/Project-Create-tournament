namespace CreateTournament.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePlayer { get; set; }
        public int IdTeam { get; set; }
        public TeamDTO Team { get; set; }
    }
}
