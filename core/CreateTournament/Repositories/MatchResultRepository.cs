using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
