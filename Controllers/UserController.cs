using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using CargoTransAPI.Models;
using CargoTransAPI.Repositories;

namespace CargoTransAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(string id)
        {
            var user = await _userRepository.GetUserAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserModel user)
        {
            var createdUser = await _userRepository.CreateUserAsync(user);
            return createdUser is null ? CreatedAtAction("Post", new { id = createdUser.UserId }, user) : StatusCode(500);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UserModel user)
        {
            user.UserId = id;
            var updatedUser = await _userRepository.UpdateUserAsync(user);
            return Ok(updatedUser);
        }
    }
}