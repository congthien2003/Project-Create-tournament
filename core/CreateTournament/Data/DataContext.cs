using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FormatType> FormatTypes { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStats>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStats)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
