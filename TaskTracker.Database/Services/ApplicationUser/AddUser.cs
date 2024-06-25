using Microsoft.Extensions.Logging;
using TaskTracker.Database.Models;

namespace TaskTracker.Database.Services.AppplicationUser
{
    public class AddUser
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<AddUser> _logger;

        public AddUser(TaskTrackerContext taskTrackerContext, ILogger<AddUser> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(ApplicationUser applicationUser, CancellationToken cancellationToken = default)
        {
            try
            {
                await _taskTrackerContext.AddAsync(applicationUser, cancellationToken);

                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogDebug("Failed to add a user with exception", ex);
                return false;
            }
        }
    }
}
