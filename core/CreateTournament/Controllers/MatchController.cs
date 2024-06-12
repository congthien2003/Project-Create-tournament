using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tourService;

        public MatchController(IMatchService matchService,
                                ITeamService teamService,
                                ITournamentService tournamentService)
        {
            _matchService = matchService;
            _teamService = teamService;
            _tourService = tournamentService;
        }
        [HttpPost("start")]
        public async Task<ActionResult> CreateStartAsync(int idTournament)
        {
            if (idTournament <= 0)
            {
                return BadRequest("Id truyền vô không hợp lệ");
            }
            var matches = await _matchService.CreateListMatchAsync(idTournament);
            if (matches.Count <= 0)
            {
                return BadRequest("Không tạo được trận đấu");
            }
            return Ok(matches);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            return Ok(match);
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllByIdTournament(int idTournament)
        {
            var matches = await _matchService.GetAllMatchesByIdTournamentAsync(idTournament);
            if (matches.Count <= 0)
            {
                return BadRequest("Các trận đấu trong giải đấu này không tồn tại ");
            }
            return Ok(matches);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateById(int id, MatchDTO matchDTO)
        {
            if (id <= 0)
            {
                return BadRequest("ID truyền vô không hợp lệ");
            }
            var team1 = await _teamService.FindTeamByIdAsync(matchDTO.IdTeam1);
            var team2 = await _teamService.FindTeamByIdAsync(matchDTO.IdTeam2);
            if (team1 == null || team2 == null || team1 == team2) { return BadRequest("Team 1, Team 2 không tồn tại hoặc 2 team trùng nhau"); }

            var matchExists = await _matchService.GetMatchByIdAsync(id);

            var match = await _matchService.UpdateMatchAsync(id, matchDTO);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            var result = await _matchService.GetMatchByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateMatch(MatchDTO matchDTO)
        {
            var tour = await _tourService.GetByIdTournament(matchDTO.TournamentId);
            if (tour == null)
            {
                return BadRequest("Giải đấu không tồn tại ");
            }
            var team1 = await _teamService.FindTeamByIdAsync(matchDTO.IdTeam1);
            var team2 = await _teamService.FindTeamByIdAsync(matchDTO.IdTeam2);
            if (team1 == null || team2 == null || team1 == team2) { 
                return BadRequest("Team 1, Team 2 không tồn tại hoặc 2 team trùng nhau"); 
            }
            var match = await _matchService.CreateAsync(matchDTO);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            return Ok(match);
        }
    }
}
