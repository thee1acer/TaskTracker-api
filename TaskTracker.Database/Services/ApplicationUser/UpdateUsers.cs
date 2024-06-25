using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public async Task<bool> ExecuteAsync(ApplicationUser applicationUser, CancellationToken cancellationToken = default)
        {
            if (applicationUser == default) return false;

            var userRecord = await _taskTrackerContext.ApplicationUsers.FirstOrDefaultAsync(v => v.Id == applicationUser.Id , cancellationToken).ConfigureAwait(false);

            if(userRecord == default) return false;

            return await UpdateUserAsync(userRecord, applicationUser);
        }

        private async Task<bool> UpdateUserAsync(ApplicationUser userRecord, ApplicationUser applicationUser)
        {
            try
            {
                userRecord.FirstName = applicationUser.FirstName;
                userRecord.LastName = applicationUser.LastName;
                userRecord.Email = applicationUser.Email;
                
                userRecord.Inactive = false;

                userRecord.ModifiedBy = 1;
                userRecord.ModifiedOn = DateTime.UtcNow;

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
