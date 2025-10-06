using CargoTransAPI.Attributes;
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
    }
}