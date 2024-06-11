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
            var match = await _context.Matches.Where(obj => obj.Id == matchResult.MatchId).FirstOrDefaultAsync();
            var tournament = await _context.Tournaments.Where(obj => obj.Id == match.TournamentId).FirstOrDefaultAsync();
            var listMatch = await _context.Matches.Where(obj => obj.TournamentId == tournament.Id).ToListAsync();

            switch (tournament.QuantityTeam)
            {
                case 4:
                    match = await CreateKnockOut4(listMatch, tournament.Id);
                    if (match == null)
                    {
                        return null;
                    }
                    break;
                case 8:
                    match = await CreateKnockOut8(listMatch, tournament.Id);
                    if (match == null)
                    {
                        return null;
                    }
                    break;
                case 16:

                    match = await CreateKnockOut16(listMatch, tournament.Id);
                    if (match == null)
                    {
                        return null;
                    }
                    break;
                case 32:
                    //match = await CreateKnockOut32(listMatch, listTeam, tournament.Id);
                    if (match == null)
                    {
                        return null;
                    }
                    break;
            }
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

        public async Task<MatchResult> UpdateAsync(int id, MatchResult matchResult, bool includeDeleted = false)
        {
            var exits = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (exits == null)
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

        public async Task<Match> CreateKnockOut4(List<Match> matches, int idTournament)
        {
            int? teamWin1 = null;
            int? teamWin2 = null;
            int? typeOfMatch = null;
            for (int i = 0; i < matches.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matches[i].Id).FirstOrDefaultAsync();
                typeOfMatch = await _context.Matches.Where(obj => obj.Id == matches[i].Id).Select(obj => obj.STT).FirstOrDefaultAsync();
                if (matchResult == null)
                {
                    continue;
                }
                switch (i)
                {
                    case 0:
                        teamWin1 = matchResult.IdTeamWin;
                        break;
                    case 1:
                        teamWin2 = matchResult.IdTeamWin;
                        break;
                }
            }

            
            if (teamWin1 != null && teamWin2 == null)
            {
                bool matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin1 && matches[j].IdTeam2 == 9999)
                    {
                        matchExists = true;
                        continue;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin1.Value,
                        IdTeam2 = 9999,
                        round = 5
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin1 == null && teamWin2 != null)
            {
                bool matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin2)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin2.Value,
                        round = 5
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin1 != null && teamWin2 != null)
            {

                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == 9999 || obj.IdTeam2 == 9999)).FirstOrDefaultAsync();
                if (match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin1.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin2.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin1.Value && obj.IdTeam2 == teamWin2.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin1.Value,
                            IdTeam2 = teamWin2.Value,
                            round = 5
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
                
            }
            return null;
        }
        public async Task<Match> CreateKnockOut8(List<Match> matches, int idTournament)
        {
            int? teamWin1 = null;
            int? teamWin2 = null;
            int? teamWin3 = null;
            int? teamWin4 = null;
            int? typeOfMatch = null;
            for (int i = 0; i < matches.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matches[i].Id).FirstOrDefaultAsync();
                typeOfMatch = await _context.Matches.Where(obj => obj.Id == matches[i].Id).Select(obj => obj.STT).FirstOrDefaultAsync();
                if (matchResult == null)
                {
                    continue;
                }
                switch (i)
                {
                    case 0:
                        teamWin1 = matchResult.IdTeamWin;
                        break;
                    case 1:
                        teamWin2 = matchResult.IdTeamWin;
                        break;
                    case 2:
                        teamWin3 = matchResult.IdTeamWin;
                        break;
                    case 3:
                        teamWin4 = matchResult.IdTeamWin;
                        break;
                }
            }

            
            if (teamWin1 != null && teamWin2 == null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin1 && matches[j].IdTeam2 == 9999)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin1.Value,
                        IdTeam2 = 9999,
                        round = 4,
                        STT = 1
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin1 == null && teamWin2 != null)
            {
                bool matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin2)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin2.Value,
                        round = 4,
                        STT = 1
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin1 != null && teamWin2 != null)
            {
                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == teamWin1 && obj.IdTeam2 == 9999 || obj.IdTeam1 == 9999 && obj.IdTeam2 == teamWin2)).FirstOrDefaultAsync();
                if(match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin1.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin2.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin1.Value && obj.IdTeam2 == teamWin2.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin1.Value,
                            IdTeam2 = teamWin2.Value,
                            STT = 1,
                            round = 4
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
            }
            //Match 2
            
            if (teamWin3 != null && teamWin4 == null)
            {
                bool matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin3 && matches[j].IdTeam2 == 9999)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin3.Value,
                        IdTeam2 = 9999,
                        STT = 2,
                        round = 4
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin3 == null && teamWin4 != null)
            {
                bool matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin4)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if (!matchExists){
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin4.Value,
                        STT = 2,
                        round = 4
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin3 != null && teamWin4 != null)
            {
                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == teamWin3 && obj.IdTeam2 == 9999 || obj.IdTeam1 == 9999 && obj.IdTeam2 == teamWin4)).FirstOrDefaultAsync();
                if(match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin3.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin4.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin3.Value && obj.IdTeam2 == teamWin4.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin3.Value,
                            IdTeam2 = teamWin4.Value,
                            STT = 2,
                            round = 4
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
                
            }
            var matchSemiFinal = await _context.Matches.Where(obj => obj.TournamentId == idTournament && obj.round == 4).ToListAsync();
            if (matchSemiFinal.Count == 2)
            {
                await CreateKnockOut4(matchSemiFinal, idTournament);
            }
            return null;
        }
        public async Task<Match> CreateKnockOut16(List<Match> matches, int idTournament)
        {
            int? teamWin1 = null;
            int? teamWin2 = null;
            int? teamWin3 = null;
            int? teamWin4 = null;
            int? teamWin5 = null;
            int? teamWin6 = null;
            int? teamWin7 = null;
            int? teamWin8 = null;
            int? typeOfMatch = null;
            for (int i = 0; i < matches.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matches[i].Id).FirstOrDefaultAsync();
                typeOfMatch = await _context.Matches.Where(obj => obj.Id == matches[i].Id).Select(obj => obj.STT).FirstOrDefaultAsync();
                if (matchResult == null)
                {
                    continue;
                }
                switch (i)
                {
                    case 0:
                        teamWin1 = matchResult.IdTeamWin;
                        break;
                    case 1:
                        teamWin2 = matchResult.IdTeamWin;
                        break;
                    case 2:
                        teamWin3 = matchResult.IdTeamWin;
                        break;
                    case 3:
                        teamWin4 = matchResult.IdTeamWin;
                        break;
                    case 4:
                        teamWin5 = matchResult.IdTeamWin;
                        break;
                    case 5:
                        teamWin6 = matchResult.IdTeamWin;
                        break;
                    case 6:
                        teamWin7 = matchResult.IdTeamWin;
                        break;
                    case 7:
                        teamWin8 = matchResult.IdTeamWin;
                        break;

                }
            }
            if (teamWin1 != null && teamWin2 == null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin1 && matches[j].IdTeam2 == 9999)
                    {
                        matchExists = true;
                        break;
                    }
                    
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin1.Value,
                        IdTeam2 = 9999,
                        STT = 3,
                        round = 1
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin1 == null && teamWin2 != null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin2.Value)
                    {
                        matchExists = true;
                        break;
                    }
                    
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin2.Value,
                        STT = 1,
                        round = 3
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin1 != null && teamWin2 != null)
            {
                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == teamWin1 && obj.IdTeam2 == 9999 || obj.IdTeam1 == 9999 && obj.IdTeam2 == teamWin2)).FirstOrDefaultAsync();
                if(match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin1.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin2.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin1.Value && obj.IdTeam2 == teamWin2.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin1.Value,
                            IdTeam2 = teamWin2.Value,
                            STT = 1,
                            round = 3
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
                
            }
            //match2
            if (teamWin3 != null && teamWin4 == null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin3 && matches[j].IdTeam2 == 9999)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if(!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin3.Value,
                        IdTeam2 = 9999,
                        STT = 2,
                        round =  3
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin3 == null && teamWin4 != null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin4)
                    {
                        matchExists = true;
                        break;
                    }
                    
                }
                if(!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin4.Value,
                        STT = 2,
                        round = 3
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin3 != null && teamWin4 != null)
            {
                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == teamWin3 && obj.IdTeam2 == 9999 || obj.IdTeam1 == 9999 && obj.IdTeam2 == teamWin4)).FirstOrDefaultAsync();
                if(match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin3.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin4.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin3.Value && obj.IdTeam2 == teamWin4.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin3.Value,
                            IdTeam2 = teamWin4.Value,
                            STT = 3,
                            round = 2
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
                
            }
            //match3
            if (teamWin5 != null && teamWin6 == null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin5 && matches[j].IdTeam2 == 9999)
                    {
                        break;
                        matchExists = true;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin5.Value,
                        IdTeam2 = 9999,
                        STT = 3,
                        round = 3
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin5 == null && teamWin6 != null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin6)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if (!matchExists){
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin6.Value,
                        STT = 3,
                        round = 3
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin5 != null && teamWin6 != null)
            {
                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == teamWin5 && obj.IdTeam2 == 9999 || obj.IdTeam1 == 9999 && obj.IdTeam2 == teamWin6)).FirstOrDefaultAsync();
                if(match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin5.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin6.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin5.Value && obj.IdTeam2 == teamWin6.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin5.Value,
                            IdTeam2 = teamWin6.Value,
                            STT = 3,
                            round = 3
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
            }
            //match4
            if (teamWin7 != null && teamWin8 == null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == teamWin7 && matches[j].IdTeam2 == 9999)
                    {
                        matchExists = true;
                        break ;
                    }
                }
                if (!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = teamWin7.Value,
                        IdTeam2 = 9999,
                        STT = 4,
                        round = 3 
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin7 == null && teamWin8 != null)
            {
                var matchExists = false;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].IdTeam1 == 9999 && matches[j].IdTeam2 == teamWin8)
                    {
                        matchExists = true;
                        break;
                    }
                }
                if(!matchExists)
                {
                    var match = new Match
                    {
                        TournamentId = idTournament,
                        IdTeam1 = 9999,
                        IdTeam2 = teamWin8.Value,
                        STT = 4,
                        round = 3
                    };
                    await _context.Matches.AddAsync(match);
                    await _context.SaveChangesAsync();
                    return match;
                }
            }
            if (teamWin7 != null && teamWin8 != null)
            {
                var match = await _context.Matches.Where(obj => obj.TournamentId == idTournament && (obj.IdTeam1 == teamWin7 && obj.IdTeam2 == 9999 || obj.IdTeam1 == 9999 && obj.IdTeam2 == teamWin8)).FirstOrDefaultAsync();
                if(match != null)
                {
                    if (match.IdTeam1 == 9999)
                    {
                        match.IdTeam1 = teamWin7.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    else if (match.IdTeam2 == 9999)
                    {
                        match.IdTeam2 = teamWin8.Value;
                        await _context.SaveChangesAsync();
                        return match;
                    }
                    var exitsmatch = await _context.Matches.Where(obj => obj.IdTeam1 == teamWin7.Value && obj.IdTeam2 == teamWin8.Value).FirstOrDefaultAsync();
                    if (exitsmatch == null)
                    {
                        var newmatch = new Match
                        {
                            TournamentId = idTournament,
                            IdTeam1 = teamWin7.Value,
                            IdTeam2 = teamWin8.Value,
                            STT = 4,
                            round = 3
                        };
                        await _context.Matches.AddAsync(newmatch);
                        await _context.SaveChangesAsync();
                        return newmatch;
                    }
                }
                
            }
            var matchQuaterFinal = await _context.Matches.Where(obj => obj.TournamentId == idTournament && obj.STT == 3).ToListAsync();
            if (matchQuaterFinal.Count == 4)
            {
                await CreateKnockOut8(matchQuaterFinal, idTournament);
            }
            return null;
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
