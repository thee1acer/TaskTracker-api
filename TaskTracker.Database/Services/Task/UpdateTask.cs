using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public async Task<bool> ExecuteAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default)
        {
            if (taskEntity == default) return false;

            var taskRecord = await _taskTrackerContext.Tasks.FirstOrDefaultAsync(v => v.Id == taskEntity.Id , cancellationToken).ConfigureAwait(false);

            if(taskRecord == default) return false;

            var result = await UpdateTaskAsync(taskRecord, taskEntity);

            return result;
        }

        private async Task<bool> UpdateTaskAsync(TaskEntity taskRecord, TaskEntity taskEntity)
        {
            try
            {
                taskRecord.AssignedTo = taskEntity.AssignedTo;
                taskRecord.ShortDescription = taskEntity.ShortDescription;
                taskRecord.DetailedDescription = taskEntity.DetailedDescription;
                taskRecord.State = taskEntity.State;
                taskRecord.TaskType = taskEntity.TaskType;
                taskRecord.TaskPriority = taskEntity.TaskPriority;
                taskRecord.TaskStoryPoints = taskEntity.TaskStoryPoints;
                taskRecord.TaskStoryEffort = taskEntity.TaskStoryEffort;

                taskRecord.ModifiedBy = taskEntity.ModifiedBy;
                taskRecord.ModifiedOn = taskEntity.ModifiedOn;


                foreach (var dbRecordTaskBlocker in taskRecord.TaskBlockers)
                {
                    var updatedTaskBlocker = taskEntity.TaskBlockers.FirstOrDefault(v => v.Id == dbRecordTaskBlocker.Id);

                    if (updatedTaskBlocker != default)
                    {
                        dbRecordTaskBlocker.OriginalTaskId = updatedTaskBlocker.OriginalTaskId;
                        dbRecordTaskBlocker.BlockerReason = updatedTaskBlocker.BlockerReason;
                        dbRecordTaskBlocker.InActive = updatedTaskBlocker.InActive;

                        dbRecordTaskBlocker.ModifiedBy = updatedTaskBlocker.ModifiedBy;
                        dbRecordTaskBlocker.ModifiedOn = updatedTaskBlocker.ModifiedOn;

                        dbRecordTaskBlocker.OriginalTask = updatedTaskBlocker.OriginalTask;
                    }
                }

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
