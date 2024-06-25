using Microsoft.AspNetCore.Mvc;
using TaskTracker.Common.Models;
using TaskTracker.Configurations;
using TaskTracker.Database.Services.UserAuthentication;
namespace UserTracker.Controllers
{
    [Route($"api/{RoutePaths.AUTHENTICATION}")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly LoginUser _userLoginService;
        private readonly RegisterUser _userRegistrationService;

        public AuthenticationController
        (
            LoginUser userLoginService,
            RegisterUser userRegistrationService,
            ILogger<AuthenticationController> logger
        )
        {
            _userLoginService = userLoginService;
            _userRegistrationService = userRegistrationService;
            _logger = logger;
        }

        [HttpPost]
        [Route("register-user")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] ApplicationUserDTO applicationUser)
        {
            try
            {
                return Ok(await _userRegistrationService.ExecuteAsync(applicationUser));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all Users with exception", ex);
                throw;
            }
        }

        [HttpPost]
        [Route("login-user/{applicationUserId:int}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> LoginUserAsync([FromBody] ApplicationUserDTO applicationUser)
        {
            try
            {
                return Ok(await _userLoginService.ExecuteAsync(applicationUser));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all Users with exception", ex);
                throw;
            }
        }
    }
}
