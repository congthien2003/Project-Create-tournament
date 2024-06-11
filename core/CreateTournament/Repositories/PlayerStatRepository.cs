using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<PlayerStats>> GetAllByIdPlayerScoreAsynsc(int id, bool includeDeleted = false)
        {
            var tour = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (tour == null)
            {
                return null;
            }

            var matchs = await _context.Matches.Where(obj => obj.TournamentId == tour.Id).ToListAsync();

            List<MatchResult> matchResults = new List<MatchResult>();
            for (int i = 0; i < matchs.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matchs[i].Id).ToListAsync();
                matchResults.AddRange(matchResult);
            }

            // Filter PlayerStats with Score > 0 before adding to the list
            List<PlayerStats> playerStats = new List<PlayerStats>();
            foreach (var matchResult in matchResults)
            {
                var playerStat = await _context.PlayerStats
                  .Where(obj => obj.MatchResultId == matchResult.Id && obj.Score > 0)
                  .ToListAsync();
                playerStats.AddRange(playerStat);
            }

            return playerStats;
        }

        public async Task<List<PlayerStats>> GetAllByIdTournamentTeam(int id,bool includeDeleted = false , int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false)
        {
            var playerStats =  _context.PlayerStats
                                     .Include(ps => ps.Player)
                                     .ThenInclude(p => p.Team).AsQueryable()
                                     .Where(ps => ps.Player.Team.TournamentId == id && !ps.IsDeleted);

            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    playerStats = playerStats.OrderBy(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    playerStats = playerStats.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                playerStats = playerStats.OrderBy(x => x.Id);
            }

            return await playerStats.Skip((currentPage - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync();

        }

        public async Task<List<PlayerStats>> GetAllByIdTournamentAsync(int id, bool includeDeleted = false)
        {
            var tour = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (tour == null)
            {
                return null;
            }
            var matchs = await _context.Matches.Where(obj => obj.TournamentId == tour.Id).ToListAsync();
            List<MatchResult> matchResults = new List<MatchResult>();
            for (int i = 0; i < matchs.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matchs[i].Id).ToListAsync();
                matchResults.AddRange(matchResult);
            }
            List<PlayerStats> playerStats = new List<PlayerStats>();
            for (int i = 0; i < matchResults.Count; i++)
            {
                var playerStat = await _context.PlayerStats.Where(obj => obj.MatchResultId == matchResults[i].Id).ToListAsync();
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
        public Expression<Func<PlayerStats, object>> GetSortColumnExpression(string? sortColumn)
        {
            switch (sortColumn)
            {
                case "score":
                    return x => x.Score;
                case "assits":
                    return x => x.Assits;
                case "yellowcard":
                    return x => x.YellowCard;
                case "redcard":
                    return x => x.RedCard;
                case null:
                    return x => x.Id; 
                default:
                    return x => x.Id;
            }
        }
        public async Task<List<PlayerStats>> Getlist(bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false)
        {
            var listPlayerStats = _context.PlayerStats.AsQueryable();

            if (!includeDeleted)
            {
                listPlayerStats = listPlayerStats.Where(obj => !obj.IsDeleted);
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                if(ascendingOrder)
                {
                    listPlayerStats = listPlayerStats.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    listPlayerStats = listPlayerStats.OrderBy(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                listPlayerStats = listPlayerStats.OrderBy(x => x.Id);
            }
            return await listPlayerStats.Skip(pageSize * currentPage - pageSize).Take(pageSize).ToListAsync();
        }

        public Int32 GetCount(string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false)
        {
            var playerstat = _context.PlayerStats.AsQueryable();
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    playerstat = playerstat.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    playerstat = playerstat.OrderBy(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                playerstat = playerstat.OrderBy(x => x.Id);
            }
            return playerstat.Count();
        }

        public async Task<List<PlayerStats>> GetAllPlayerStatsByTournamentAsync(int id, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = true)
        {

            var matches = await _context.Matches
                                    .Where(m => m.TournamentId == id && !m.IsDeleted)
                                    .Select(m => m.Id)
                                    .ToListAsync();

            var matchResults = await _context.MatchResults
                                             .Where(mr => matches.Contains(mr.MatchId) && !mr.IsDeleted)
                                             .Select(mr => mr.Id)
                                             .ToListAsync();

            var playerStats = _context.PlayerStats
                                            .Where(ps => matchResults.Contains(ps.MatchResultId) && !ps.IsDeleted)
                                            .Include(ps => ps.Player)
                                            .ThenInclude(p => p.Team).AsQueryable();
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    playerStats = playerStats.OrderBy(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    playerStats = playerStats.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                playerStats = playerStats.OrderBy(x => x.Id);
            }

            return await playerStats
                          .Skip((currentPage - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync();


        }
    }
}

