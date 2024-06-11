using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string currentPage = "1", string pageSize = "5") {

            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);

            var list = await _userService.GetList(_currentPage, _pageSize, false);
            var count = await _userService.GetCountList();
            var _totalPage = count % _pageSize == 0 ? count / _pageSize : count / _pageSize + 1;
            var result = new
            {
                list,
                _currentPage,
                _pageSize,
                _totalPage,
                _totalRecords = count,
                _hasNext = _currentPage < _totalPage,
                _hasPre = _currentPage > 1,
            };
            return Ok(result);
        }

        [HttpGet("getCount")]
        public async Task<IActionResult> GetCount() {
            var count = await _userService.GetCountList();
            return Ok(count);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetById(id);
            return user;
        }

        [HttpPut("update")]
        public async Task<UserDTO> Update(UserDTO user)
        {
            var update = await _userService.Edit(user);
            return update;
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _userService.Delete(id);
            if (delete == true)
            {
                return Ok(delete);
            }
            return BadRequest();
        }

    }
}
