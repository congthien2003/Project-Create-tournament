
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService )
        {
            _teamService = teamService;
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(int number, int idTournament)
        {
            if (number <= 0)
            {
                return BadRequest("Số team truyền vô không hợp lệ");
            }
            for (int i = 1; i <= number; i++)
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
        public async Task<ActionResult> GetAllByIdTournament(int idTornament)
        {
            var teams = await _teamService.GetAllByIdTournamentAsync(idTornament);
            if(teams.Count == 0)
            {
                return BadRequest("Các trận đấu không tồn tại");
            }
            return Ok(teams);
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateTeam(int id, string name)
        {

            var team = await _teamService.UpdateAsync(id,name );
            if (team == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }
            return Ok(team);
        }
    }
}
