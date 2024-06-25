using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTracker.Database.Models;

namespace TaskTracker.Database.Services.AppplicationUser
{
    public class GetAllUsers
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<GetAllUsers> _logger;

        public GetAllUsers(TaskTrackerContext taskTrackerContext, ILogger<GetAllUsers> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<List<ApplicationUser>> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await _taskTrackerContext.ApplicationUsers.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
