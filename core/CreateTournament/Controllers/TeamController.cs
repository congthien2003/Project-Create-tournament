
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService )
        {
            _teamService = teamService;
        }
        [HttpPost("create")]

        public async Task<ActionResult> CreateAsync(int quantity, int idTournament)
        {
            if (quantity <= 0)
            {
                return BadRequest("Số team truyền vô không hợp lệ");
            }
            for (int i = 1; i <= quantity; i++)
            {
                var team = new TeamDTO
                {
                    Name = $"#{i}",
                    TournamentId = idTournament,
                };
                await _teamService.CreateAsync(team);
            }
            return Ok();
            
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllByIdTournament(int idTournament)
        {
            var teams = await _teamService.GetAllByIdTournamentAsync(idTournament);
            if(teams.Count == 0)
            {
                return BadRequest("Các trận đấu không tồn tại");
            }
            return Ok(teams);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {

            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            return Ok(team);
        }

        [HttpGet("GetListTeamSwap")]
        public async Task<ActionResult> GetListTeamSwap(int idTournament, int round)
        {

            var listIdTeam = await _teamService.GetListTeamSwap(idTournament, round);
            return Ok(listIdTeam);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateTeam(int id, string name)
        {

            var team = await _teamService.UpdateAsync(id, name);
            if (team == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            return Ok(team);
        }

        [HttpPut("updateImage")]
        public async Task<ActionResult> UpdateImage(int id, string path)
        {

            var team = await _teamService.UpdateImage(id, path);
            if (team == null)
            {
                return BadRequest("Đội không tồn tại");
            }
            return Ok(team);
        }


    }
}
