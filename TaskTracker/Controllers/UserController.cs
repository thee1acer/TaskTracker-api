using Microsoft.AspNetCore.Mvc;
using TaskTracker.Configurations;
using TaskTracker.Database.Models;
using TaskTracker.Database.Services.AppplicationUser;
namespace UserTracker.Controllers
{
    [Route($"api/{RoutePaths.APPLICATION_USER}")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly AddUser _addUserService;
        private readonly DeleteUser _deleteUserService;
        private readonly GetAllUsers _getAllUsersService;
        private readonly UpdateUser _updateUserService;

        public UserController
        (
            AddUser addUserService,
            DeleteUser deleteUserService,
            GetAllUsers getAllUsersService,
            UpdateUser updateUserService,
            ILogger<UserController> logger
        )
        {
            _logger = logger;
            _addUserService = addUserService;
            _deleteUserService = deleteUserService;
            _getAllUsersService = getAllUsersService;
            _updateUserService = updateUserService;
        }

        [HttpGet]
        [Route("get-all-users")]
        [ProducesResponseType(typeof(List<ApplicationUser>), 200)]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _getAllUsersService.ExecuteAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all Users with exception", ex);
                throw;
            }
        }

        [HttpPost]
        [Route("update-User")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] ApplicationUser ApplicationUser)
        {
            try
            {
                return Ok(await _updateUserService.ExecuteAsync(ApplicationUser));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update all User with exception", ex);
                return Ok(false);
            }
        }

        [HttpDelete]
        [Route("delete-User/{UserId:int}")]
        [ProducesResponseType(typeof(List<ApplicationUser>), 200)]
        public async Task<IActionResult> DeleteUserAsync(int UserId)
        {
            try
            {
                return Ok(await _deleteUserService.ExecuteAsync(UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete User with exception", ex);
                return Ok(false);
            }
        }


        [HttpPost]
        [Route("add-User")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> AddUserAsync([FromBody] ApplicationUser ApplicationUser)
        {
            try
            {
                return Ok(await _addUserService.ExecuteAsync(ApplicationUser));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add User with exception", ex);
                return Ok(false);
            }
        }

    }
}
