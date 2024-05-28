using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class MatchRepository : IMatchRepository<Match>
    {
        private readonly DataContext _context;

        public MatchRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Match>> CreateListMatchAsync(int idTournament)
        {
            var list = await _context.Teams.Where(o => o.TournamentId == idTournament).ToListAsync();
            if(list.Count <= 0)
            {
                return null;
            }
            for(int i = 0;i<list.Count; i+=2)
            {
                var match = new Match
                {
                    IdTeam1 = list[i].Id,
                    IdTeam2 = list[i+1].Id,
                    TournamentId = idTournament,
                    StartAt = DateTime.Now,
                    Created = DateTime.Now,
                };
                await _context.AddAsync(match);
                await _context.SaveChangesAsync();
            }
            var matchs = await _context.Matches.Where(o=> o.TournamentId == idTournament).ToListAsync();
            return matchs;
        }

        public async Task<Match> GetMatchByIdAsync(int id, bool includeDeleted = false)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(o => o.Id == id && o.IsDeleted == includeDeleted);
            if(match == null)
            {
                return null;
            }
            return match;
        }

        public async Task<List<Match>> GetAllMatchesByIdTournamentAsync(int idTournament, bool includeDeleted = false)
        {
            var matches = await _context.Matches.Where(o => o.TournamentId == idTournament && o.IsDeleted == includeDeleted).ToListAsync();
            if(matches == null)
            {
                return null;
            }
            return matches;
        }

        public async Task<Match> UpdateMatchByIdAsync(int id, Match match, bool includeDeleted = false)
        {
            var exits = await _context.Matches.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == includeDeleted);
            if(exits == null)
            {
                return null;
            }
            else
            {
                exits.IdTeam1 = match.IdTeam1;
                exits.IdTeam2 = match.IdTeam2;
                exits.StartAt = match.StartAt;
            }
            await _context.SaveChangesAsync();
            return exits;
        }

        public async Task<Match> CreateAsync(Match match)
        {
            if(match == null)
            {
                return null;
            }
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
            return match;
        }
    }   
}
