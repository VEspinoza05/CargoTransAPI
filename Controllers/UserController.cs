using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using CargoTransAPI.Models;
using CargoTransAPI.Repositories;
using CargoTransAPI.Attributes;
using CargoTransAPI.DTOs;
using FirebaseAdmin.Auth;

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

        /* These routes were Uncommented because at the moment, they are not necessary
        [HttpGet]
        [FirebaseAuthorize("Administrador")] 
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
        */

        [HttpPost]
        [FirebaseAuthorize("Administrador")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDTO request)
        {
            try
            {
                var userArgs = new UserRecordArgs()
                {
                    Email = request.Email,
                    Password = request.Password,
                    DisplayName = request.DisplayName,
                    EmailVerified = false,
                    Disabled = false,
                };

                var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

                var claims = new Dictionary<string, object>()
                {
                    { "role", request.Role },
                    { "branchCity", request.branchCity }
                };

                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);

                return Ok(new
                {
                    Message = "Usuario creado exitosamente",
                    Uid = userRecord.Uid,
                    Email = userRecord.Email,
                    Role = request.Role
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /* These routes were Uncommented because at the moment, they are not necessary
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UserModel user)
        {
            user.UserId = id;
            var updatedUser = await _userRepository.UpdateUserAsync(user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }
        */
    }
}