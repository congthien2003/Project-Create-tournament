using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            var FormatTypeId = await _context.Tournaments.Where(o => o.Id == idTournament).Select(o=>o.FormatTypeId).FirstOrDefaultAsync();
            switch (FormatTypeId)
            {
                case 1:
                    await CreateMatchsKnockOutAsync(idTournament, list);
                    break;
                case 2:
                    await CreateMatchsLeagueAsync(idTournament, list);
                    break;
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
        public async Task<List<Match>> CreateMatchsKnockOutAsync(int idTournament, List<Team> teams)
        {
            var stt = 1;
            if (teams.Count <= 0)
            {
                return null;
            }
            for (int i = 0; i < teams.Count; i += 2)
            {

                var match = new Match
                {
                    IdTeam1 = teams[i].Id,
                    IdTeam2 = teams[i + 1].Id,
                    TournamentId = idTournament,
                    StartAt = DateTime.Now,
                    Created = DateTime.Now,
                    STT = stt++
                };
                if(teams.Count == 4)
                {
                    match.round = 4;
                }
                if(teams.Count == 8)
                {
                    match.round = 3;
                }
                if(teams.Count == 16)
                {
                    match.round = 2;
                }
                await _context.AddAsync(match);
                await _context.SaveChangesAsync();
            }
            var matchs = await _context.Matches.Where(o => o.TournamentId == idTournament).ToListAsync();
            return matchs;
        }
        public async Task<List<Match>> CreateMatchsLeagueAsync(int idTournament, List<Team> teams)
        {
            if (teams.Count <= 0)
            {
                return null;
            }
            var matchesToAdd = new List<Match>();
            for (int team1 = 1; team1 <= teams.Count; team1++)
            {
                for (int team2 = 1;team2 <= teams.Count; team2++)
                {
                    if(team1 == team2)
                    {
                        continue;
                    }
                    else
                    {
                        var match = new Match
                        {
                            IdTeam1 = team1,
                            IdTeam2 = team2,
                            TournamentId = idTournament,
                            StartAt = DateTime.Now,
                            Created = DateTime.Now,
                            STT = 6
                        };
                        matchesToAdd.Add(match);
                    }
                }
                
            }
            await _context.AddRangeAsync(matchesToAdd);
            await _context.SaveChangesAsync();
            
            return matchesToAdd;
        }
    }   
}
