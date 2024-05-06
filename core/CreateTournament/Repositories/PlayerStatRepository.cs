using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class PlayerStatRepository : IPlayerStatRepository<PlayerStats>
    {
        private readonly DataContext _context;

        public PlayerStatRepository(DataContext dataContext)
        {
            _context = dataContext;

        }
        public async Task<PlayerStats> CreateAsync(PlayerStats playerStats)
        {
            await _context.PlayerStats.AddAsync(playerStats);
            await _context.SaveChangesAsync();
            return playerStats;
        }

        public async Task<List<PlayerStats>> GetAllByIdMatchAsync(int id, bool includeDeleted = false)
        {
            var playerStats = await _context.PlayerStats.Where(obj => obj.MatchResultId == id && obj.IsDeleted == includeDeleted).ToListAsync();
            if(playerStats.Count ==0)
            {
                return null;
            }
            return playerStats;
        }

        public async Task<List<PlayerStats>> GetAllByIdPlayerAsync(int id, bool includeDeleted = false)
        {
            var playerStats = await _context.PlayerStats.Where(obj => obj.PlayerId == id && obj.IsDeleted == includeDeleted).ToListAsync();
            if (playerStats.Count == 0)
            {
                return null;
            }
            return playerStats;
        }

        public async Task<List<PlayerStats>> GetAllByIdTournamentAsync(int id, bool includeDeleted = false)
        {
            var tour = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (tour == null)
            {
                return null;
            }
            var matchs = await _context.Matches.Where(obj => obj.TouramentId == tour.Id).ToListAsync();
            List<MatchResult> matchResults = new List<MatchResult>();
            for (int i=0;i<matchs.Count;i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matchs[i].Id).ToListAsync();
                matchResults.AddRange(matchResult);
            }
            List<PlayerStats> playerStats = new List<PlayerStats>();
            for(int i=0;i<matchResults.Count;i++)
            {
                var playerStat = await _context.PlayerStats.Where(obj =>obj.MatchResultId == matchResults[i].Id).ToListAsync(); 
                playerStats.AddRange(playerStat);
            }
            return playerStats;
        }

        public async Task<PlayerStats> GetByIdAsync(int id, bool inculdeDeleted = false)
        {
            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == inculdeDeleted);
            if(playerStat == null)
            {
                return null;
            }
            return playerStat;
        }

        public async Task<PlayerStats> GetByIdMatchAndIdPlayerAsync(int idPlayer, int idMatch, bool inculdeDeleted = false)
        {
            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(obj => obj.PlayerId == idPlayer && obj.MatchResultId == idMatch && obj.IsDeleted == inculdeDeleted);
            if (playerStat == null)
            {
                return null;
            }
            return playerStat;
        }

        public async Task<PlayerStats> UpdateByIdAsync(int id, PlayerStats playerStats, bool includeDeleted = false)
        {
            var exits = await _context.PlayerStats.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == false);
            if(exits == null)
            {
                return null;
            }
            else
            {
                exits.Score = playerStats.Score;
                exits.Assits = playerStats.Assits;
                exits.YellowCard = playerStats.YellowCard;
                exits.RedCard = playerStats.RedCard;
            }
            await _context.SaveChangesAsync();
            return exits;
        }
    }
}
