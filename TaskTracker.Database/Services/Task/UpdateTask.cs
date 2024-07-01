using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTracker.Common.Models.Task;
using TaskTracker.Database.Models.Task;

namespace TaskTracker.Database.Services.Task
{
    public class UpdateTask
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<UpdateTask> _logger;

        public UpdateTask(TaskTrackerContext taskTrackerContext, ILogger<UpdateTask> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(TaskEntityDTO taskEntity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (taskEntity == default) return false;

                var taskRecord = await _taskTrackerContext.Tasks.FirstOrDefaultAsync(v => v.Id == taskEntity.Id, cancellationToken)
                        .ConfigureAwait(false);

                if (taskRecord == default) return false;

                var taskDbRecord = taskEntity.Adapt<TaskEntity>();

                _taskTrackerContext.Entry(taskRecord)
                                .CurrentValues
                                .SetValues(taskDbRecord);

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

