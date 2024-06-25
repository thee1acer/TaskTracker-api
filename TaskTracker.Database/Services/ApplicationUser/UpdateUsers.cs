using Mapster;
using Microsoft.Extensions.Logging;
using TaskTracker.Common.Models;
using TaskTracker.Database.Models;

namespace TaskTracker.Database.Services.AppplicationUser
{
    public class UpdateUser
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<UpdateUser> _logger;

        public UpdateUser(TaskTrackerContext taskTrackerContext, ILogger<UpdateUser> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(ApplicationUserDTO applicationUser, CancellationToken cancellationToken = default)
        {
            try
            {
                if (applicationUser == default) return false;

                var applicationUserRecord = applicationUser.Adapt<ApplicationUser>();

                _taskTrackerContext.ApplicationUsers.Update(applicationUserRecord);

                await _taskTrackerContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Failed to update with Exception: ", ex);
                return false;
            }
        }
    }
}
