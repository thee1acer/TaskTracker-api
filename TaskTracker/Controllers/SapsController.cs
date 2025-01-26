using Microsoft.AspNetCore.Mvc;
using TaskTracker.Configurations;
using TaskTracker.Database.Services.Task;
using TaskTracker.Common.Models.Task;
using TaskTracker.Common.Models.Saps;
using TaskTracker.Database.Services.Saps;

namespace TaskTracker.Controllers
{
    [Route($"api/{RoutePaths.SAPS}")]
    [ApiController]
    public class SapsController : ControllerBase
    {

        private readonly ILogger<SapsController> _logger;
        private readonly WebScraperService _webScraperService;

        public SapsController (WebScraperService webScraperService, ILogger<SapsController> logger)
        {
            _webScraperService = webScraperService;
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

        [HttpGet]
        [Route("process/cases/missing-persons")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> ScrapeAndSaveMissingPersonsCases(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _webScraperService.FetchAllMissingPersonCasesAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all subjects with exception {ex}");
                throw;
            }
        }

        [HttpGet]
        [Route("process/cases/wanted-persons")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> ScrapeAndSaveWantedPersonsCases(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _webScraperService.FetchAllWantedPersonCasesAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all subjects with exception {ex}");
                throw;
            }
        }

    }
}
