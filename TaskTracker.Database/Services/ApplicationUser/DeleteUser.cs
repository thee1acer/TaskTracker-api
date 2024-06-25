using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TaskTracker.Database.Services.AppplicationUser
{
    public class DeleteUser
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<DeleteUser> _logger;

        public DeleteUser(TaskTrackerContext taskTrackerContext, ILogger<DeleteUser> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _taskTrackerContext.ApplicationUsers.FirstOrDefaultAsync(v => v.Id == userId, cancellationToken)
                        .ConfigureAwait(false);

                if (user == default) return false;
                
                user.Inactive = true;

                await _taskTrackerContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogDebug("Failed to delete a user with exception", ex);
                return false;
            }
        }
    }
}
