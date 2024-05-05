using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Repositories;
using CreateTournament.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ITournamentRepository<Tournament> _tournamentRepository;

        public TournamentController(ITournamentService tournamentService, ITournamentRepository<Tournament> tournamentRepository)
        {
            _tournamentService = tournamentService;
            _tournamentRepository = tournamentRepository;
        }

        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var tournament = await _tournamentService.GetAll();
            return Ok(tournament);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetByIdTournament(int id)
        {
            var tournament = await _tournamentService.GetByIdTournament(id);
            if (tournament == null)
            {
                return BadRequest();
            }
            return Ok(tournament);
        }

        [HttpGet("listTournament")]
        public async Task<ActionResult> GetTourByUserId(int userId)
        {
            var tournament = await _tournamentService.GetTourByUserId(userId);
            return Ok(tournament);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(TournamentDTO tournamentDTO)
        {
            var newtournament = await _tournamentService.Create(tournamentDTO);
            return Ok(newtournament);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(TournamentDTO tournamentDTO)
        {
            var tournament = await _tournamentService.Update(tournamentDTO);
            if (tournament == null)
            {
                return BadRequest();
            }
            return Ok(tournament);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Deleted(int id)
        {
            var sportType = await _tournamentRepository.GetByIdTournament(id);
            if (sportType == null || sportType.IsDeleted)
            {
                return BadRequest("Failed to delete FormatType");
            }
            else
            {
                await _tournamentService.Delete(id);
                return Ok(true);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchTournaments(string searchTerm)
        {
            // Kiểm tra từ khóa tìm kiếm
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required");
            }
            var tournaments = await _tournamentService.SearchTournaments(searchTerm);
            return Ok(tournaments);
        }
    }
}
