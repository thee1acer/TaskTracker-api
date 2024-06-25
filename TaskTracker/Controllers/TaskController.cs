using Microsoft.AspNetCore.Mvc;
using TaskTracker.Configurations;
using TaskTracker.Database.Models.Task;
using TaskTracker.Database.Services.Task;

namespace TaskTracker.Controllers
{
    [Route($"api/{RoutePaths.TASK}")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly AddTask _addTaskService;
        private readonly DeleteTask _deleteTaskService;
        private readonly GetAllTasks _getAllTasksService;
        private readonly UpdateTask _updateTaskService;

        public TaskController
        (
            AddTask addTaskService,
            DeleteTask deleteTaskService,
            GetAllTasks getAllTasksService,
            UpdateTask updateTaskService,
            ILogger<TaskController> logger
        )
        {
            _logger = logger;
            _addTaskService = addTaskService;
            _deleteTaskService = deleteTaskService;
            _getAllTasksService = getAllTasksService;
            _updateTaskService = updateTaskService;
        }

        [HttpGet]
        [Route("get-all-tasks")]
        [ProducesResponseType(typeof(List<TaskEntity>), 200)]
        public async Task<IActionResult> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _getAllTasksService.ExecuteAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all tasks with exception", ex);
                throw;
            }
        }

        [HttpPost]
        [Route("update-task")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateTaskAsync([FromBody] TaskEntity taskEntity)
        {
            try
            {
                return Ok(await _updateTaskService.ExecuteAsync(taskEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update all task with exception", ex);
                return Ok(false);  
            }
        }

        [HttpDelete]
        [Route("delete-task/{taskId:int}")]
        [ProducesResponseType(typeof(List<TaskEntity>), 200)]
        public async Task<IActionResult> DeleteTaskAsync(int taskId)
        {
            try
            {
                return Ok(await _deleteTaskService.ExecuteAsync(taskId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete task with exception", ex);
                return Ok(false);
            }
        }


        [HttpPost]
        [Route("add-task")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> AddTaskAsync([FromBody] TaskEntity taskEntity)
        {
            try
            {
                return Ok(await _addTaskService.ExecuteAsync(taskEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add task with exception", ex);
                return Ok(false);
            }
        }

    }
}
