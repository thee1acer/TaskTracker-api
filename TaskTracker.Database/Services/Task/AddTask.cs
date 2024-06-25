using Mapster;
using Microsoft.Extensions.Logging;
using TaskTracker.Common.Models.Task;
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

        public async Task<bool> ExecuteAsync(TaskEntityDTO taskEntity, CancellationToken cancellationToken = default)
        {
            try
            {
                var taskDbRecord = taskEntity.Adapt<TaskEntity>();

                if (taskDbRecord == null)
                {
                    await _taskTrackerContext.AddAsync(taskEntity, cancellationToken);

                    await _taskTrackerContext.SaveChangesAsync(cancellationToken);
                    return true;
                }

                return false;
            }
            catch(Exception ex) 
            {
                _logger.LogDebug("Failed to add a Task with exception", ex);
                return false;
            }
        }
    }
}
