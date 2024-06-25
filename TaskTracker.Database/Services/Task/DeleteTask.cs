using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace TaskTracker.Database.Services.Task
{
    public class DeleteTask
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<DeleteTask> _logger;

        public DeleteTask(TaskTrackerContext taskTrackerContext, ILogger<DeleteTask> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(int taskId, CancellationToken cancellationToken = default)
        {
            try
            {
                var task = await _taskTrackerContext.Tasks.FirstOrDefaultAsync(v => v.Id == taskId, cancellationToken).ConfigureAwait(false);

                if (task == default) return false;

                
                task.State = "Deffered";
                //task.ModifiedBy = 1; - to be modified
                task.ModifiedOn = DateTime.UtcNow;

                await _taskTrackerContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
                
            }
            catch(Exception ex) 
            {
                _logger.LogDebug("Failed to delete a Task with exception", ex);
                return false;
            }
        }
    }
}
