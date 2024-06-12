using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MatchResultController : ControllerBase
    {
        private readonly IMatchResultService _matchResult;
        private readonly IMatchService _matchService;
        private readonly ITournamentService _tournamentService;

        public MatchResultController(IMatchResultService matchResultService,
                                     IMatchService matchService, ITournamentService tournamentService)
        {
            _matchResult = matchResultService;
            _matchService = matchService;
            _tournamentService = tournamentService;

        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(MatchResultDTO matchResultDTO)
        {
            var match = await _matchService.GetMatchByIdAsync(matchResultDTO.MatchId);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            var team1 = match.IdTeam1;
            var team2 = match.IdTeam2;
            if (matchResultDTO.IdTeamWin != team1 && matchResultDTO.IdTeamWin != team2)
            {
                return BadRequest("Đội thắng không tồn tại trong trận đấu này");
            }
            if (matchResultDTO.ScoreT1 == matchResultDTO.ScoreT2 || matchResultDTO.ScoreT2 < 0 || matchResultDTO.ScoreT1 < 0)
            {
                return BadRequest("Kết quả trận đấu không hợp lệ");
            }
            else if (matchResultDTO.ScoreT1 > matchResultDTO.ScoreT2)
            {
                if (matchResultDTO.IdTeamWin == team2)
                {
                    return BadRequest("Đội thắng không hợp lệ");
                }
            }
            else if (matchResultDTO.ScoreT1 < matchResultDTO.ScoreT2)
            {
                if (matchResultDTO.IdTeamWin == team1)
                {
                    return BadRequest("Đội thắng không hợp lệ");
                }
            }
            var matchResult = await _matchResult.CreateAsync(matchResultDTO);
            if (matchResult == null)
            {
                return BadRequest("Không thể cập nhật kết quả trận đấu");
            }
            return Ok(matchResult);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdMatchResult(int id)
        {
            var matchResult = await _matchResult.GetMatchResultById(id);
            if (matchResult == null)
            {
                return BadRequest("Không thể tìm thấy kết quả trận đấu");
            }
            return Ok(matchResult);
        }
        [HttpGet("/api/MatchResult/idMatch/{idMatch:int}")]
        public async Task<ActionResult> GetByIdMatch(int idMatch)
        {
            var matchResult = await _matchResult.GetMatchResultByIdMatch(idMatch);
            if (matchResult == null)
            {
                return BadRequest("Trận đấu chưa có kết quả");
            }
            return Ok(matchResult);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateById(int id, MatchResultDTO matchResultDTO)
        {
            var matchResult = await _matchResult.GetMatchResultById(id);
            if (matchResult == null)
            {
                return BadRequest("Trận đấu chưa có kết quả");
            }
            matchResult = matchResultDTO;
            var match = await _matchService.GetMatchByIdAsync(matchResult.MatchId);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            var team1 = match.IdTeam1;
            var team2 = match.IdTeam2;
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            if (matchResult.IdTeamWin != team1 && matchResult.IdTeamWin != team2)
            {
                return BadRequest("Đội thắng không tồn tại trong trận đấu này");
            }
            if (matchResult.ScoreT2 < 0 || matchResult.ScoreT1 < 0)
            {
                return BadRequest("Kết quả trận đấu không hợp lệ");
            }
            else if (matchResult.ScoreT1 > matchResult.ScoreT2)
            {
                if (matchResult.IdTeamWin == team2)
                {
                    return BadRequest("Đội thắng không hợp lệ");
                }
            }
            else if (matchResult.ScoreT1 < matchResult.ScoreT2)
            {
                if (matchResult.IdTeamWin == team1)
                {
                    return BadRequest("Đội thắng không hợp lệ");
                }
            }
            await _matchResult.UpdateAsync(id, matchResult);
            return Ok(matchResult);
        }

        [HttpPut("/api/MatchResult/1/{id:int}")]
        public async Task<ActionResult> UpdateKnockOut(int id, MatchResultDTO matchResultDTO)
        {
            return Ok();
        }
    }

}
