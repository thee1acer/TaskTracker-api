using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTracker.Common.Enums;
using TaskTracker.Common.Models.Task;

namespace TaskTracker.Database.Services.Task
{
    public class GetAllTasks
    {
        private TaskTrackerContext _taskTrackerContext;
        public ILogger<GetAllTasks> _logger;

        public GetAllTasks(TaskTrackerContext taskTrackerContext, ILogger<GetAllTasks> logger)
        {
            _taskTrackerContext = taskTrackerContext;
            _logger = logger;
        }

        public async Task<List<TaskEntityDTO>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var tasks = await _taskTrackerContext.Tasks.Include(v => v.TaskBlockers).Where(v => v.State != TaskStatusEnum.Removed.Description()).ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

            return tasks.Adapt<List<TaskEntityDTO>>();
        }
    }
}
