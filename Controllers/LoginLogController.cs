using CargoTransAPI.Attributes;
using CargoTransAPI.DTOs;
using CargoTransAPI.Extensions;
using CargoTransAPI.Models;
using CargoTransAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CargoTransAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginLogController : ControllerBase
    {
        private readonly LoginLogRepository _loginLogRepository;

        public LoginLogController(LoginLogRepository loginLogRepository)
        {
            _loginLogRepository = loginLogRepository;
        }

        [HttpGet]
        [FirebaseAuthorize("Administrador")]
        public async Task<IActionResult> GetAllLoginLogs()
        {
            var loginLogs = await _loginLogRepository.GetLoginLogsAsync();
            return Ok(loginLogs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoginLog()
        {
            LoginLogModel loginLog = new LoginLogModel()
            {
                UserId = HttpContext.GetUserId(),
                Username = HttpContext.GetUsername(),
                UserEmail = HttpContext.GetUserEmail(),
                Timestamp = HttpContext.GetUserIssueTokenDate() ?? new DateTime(),
                Role = HttpContext.GetUserRole(),
                BranchCity = HttpContext.GetUserBranchCity(),
            };
            

            await _loginLogRepository.CreateLoginLogAsync(loginLog);
            return Ok();
        }
    }
}