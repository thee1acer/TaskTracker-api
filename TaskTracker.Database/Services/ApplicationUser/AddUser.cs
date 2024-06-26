using Mapster;
using Microsoft.Extensions.Logging;
using TaskTracker.Common.Models;
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

        public async Task<bool> ExecuteAsync(ApplicationUserDTO applicationUser, CancellationToken cancellationToken = default)
        {
            try
            {
                var applicationUserDbRecord = applicationUser.Adapt<ApplicationUser>();

                //check first if a record does not exist

                await _taskTrackerContext.AddAsync(applicationUserDbRecord, cancellationToken).ConfigureAwait(false); 

                await _taskTrackerContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

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
