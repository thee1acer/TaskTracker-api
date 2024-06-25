using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTracker.Database.Models.Task;

namespace TaskTracker.Database.Services.Task
{
    public class AddTask
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<AddTask> _logger;

        public AddTask(TaskTrackerContext taskTrackerContext, ILogger<AddTask> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _taskTrackerContext.AddAsync(taskEntity, cancellationToken);

                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogDebug("Failed to add a Task with exception", ex);
                return false;
            }
        }
    }
}
