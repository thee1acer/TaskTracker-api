using Microsoft.AspNetCore.Mvc;
using TaskTracker.Configurations;
using TaskTracker.Database.Services.Task;
using TaskTracker.Common.Models.Task;
using TaskTracker.Common.Models.Saps;

namespace TaskTracker.Controllers
{
    [Route($"api/{RoutePaths.SAPS}")]
    [ApiController]
    public class SapsController : ControllerBase
    {

        private readonly ILogger<SapsController> _logger;

        public SapsController
        (
            ILogger<SapsController> logger
        )
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("cases")]
        [ProducesResponseType(typeof(List<CaseDTO>), 200)]
        public async Task<IActionResult> GetAllCases(CancellationToken cancellationToken)
        {
            try
            {
                //return Ok(await _sapsService.ExecuteGetAllCasesAsync(cancellationToken));
                return Ok(new List<CaseDTO>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all subjects with exception {ex}");
                throw;
            }
        }

    }
}
