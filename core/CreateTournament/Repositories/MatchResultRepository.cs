using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreateTournament.Repositories
{
    public class MatchResultRepository : IMatchResultRepository<MatchResult>
    {
        private readonly DataContext _context;

        public MatchResultRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<MatchResult> CreateAsync(MatchResult matchResult)
        {
            await _context.MatchResults.AddAsync(matchResult);
            await _context.SaveChangesAsync();
            return matchResult;
        }
        public Expression<Func<MatchResult, object>> GetSortColumnExpression(string? sortColumn)
        {
            switch (sortColumn)
            {
                case "scoret1":
                    return x => x.ScoreT1;
                case "scoret2":
                    return x => x.ScoreT2;
                case "idteamwin":
                    return x => x.IdTeamWin;
                case null:
                    return x => x.Id;
                default:
                    return x => x.Id;
            }
        }
        public async Task<List<MatchResult>> GetAllMatchResults(int idtour, bool includeDeleted = false)
        {
            var tournament = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == idtour && obj.IsDeleted == includeDeleted);
            if (tournament == null)
            {
                return null;
            }
            var matchs = await _context.Matches.Where(obj => obj.TournamentId == idtour).ToListAsync();
            List<MatchResult> matchResults = new List<MatchResult>();
            for (int i = 0; i < matchs.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matchs[i].Id).ToListAsync();
                matchResults.AddRange(matchResult);
            }
            return matchResults;
        }

        public async Task<List<MatchResult>> GetAllMatchResultsScore(bool includeDeleted = false)
        {
            return await _context.MatchResults
            .Where(obj => includeDeleted || !obj.IsDeleted && obj.ScoreT1 > 0 && obj.ScoreT2 > 0)
            .OrderByDescending(p => p.ScoreT1).Include("PlayerStats")
            .ToListAsync();
        }

        public async Task<MatchResult> GetMatchResultById(int id, bool includeDeleted = false)
        {
            var matchResult = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (matchResult == null)
            {
                return null;
            }
            return matchResult;

        }

        public async Task<MatchResult> GetMatchResultByIdMatch(int id, bool includeDeleted = false)
        {
            var matchResult = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.MatchId == id && obj.IsDeleted == includeDeleted);
            if (matchResult == null)
            {
                return null;
            }
            return matchResult;
        }

        public async Task<MatchResult> UpdateAsync(int id,MatchResult matchResult, bool includeDeleted = false)
        {
            var exits = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if(exits == null)
            {
                return null;
            }
            else
            {
                exits.ScoreT1 = matchResult.ScoreT1;
                exits.ScoreT2 = matchResult.ScoreT2;
                exits.IdTeamWin = matchResult.IdTeamWin;
            }
            await _context.SaveChangesAsync();
            return exits;
        }

        public async Task<List<MatchResult>> Getlist(int idtour, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false)
        {
            var tournament = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == idtour && obj.IsDeleted == includeDeleted);
            if (tournament == null)
            {
                return null;
            }

            var listMatchResults = _context.MatchResults.AsQueryable().Where(obj => obj.Match.TournamentId == idtour);

            if (!includeDeleted)
            {
                listMatchResults = listMatchResults.Where(obj => !obj.IsDeleted);
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    listMatchResults = listMatchResults.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    listMatchResults = listMatchResults.OrderBy(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                listMatchResults = listMatchResults.OrderBy(x => x.Id);
            }
            return await listMatchResults.Skip(pageSize * currentPage - pageSize).Take(pageSize).ToListAsync();
        }

        public Int32 GetCount(int idtour,string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false)
        {
            var matchresult = _context.MatchResults.AsQueryable().Where(obj => obj.Match.TournamentId == idtour);
            if (!incluDeleted)
            {
                matchresult = matchresult.Where(obj => !obj.IsDeleted);
            }
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    matchresult = matchresult.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    matchresult = matchresult.OrderBy(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                matchresult = matchresult.OrderBy(x => x.Id);
            }
            return matchresult.Count();
        }
    }
}
