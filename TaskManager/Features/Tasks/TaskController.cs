using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManager.Features.Tasks
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet("filter")]
        public async Task<IActionResult> GetTasks(
            [FromQuery] bool? isCompleted,
            [FromQuery] string? priority,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] string? search)
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetFilteredTasksAsync(userId, isCompleted, priority, startDate, endDate, search);
            return Ok(tasks);
            }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id, GetUserId());
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskRequest request)
        {
            var task = await _taskService.CreateTaskAsync(request, GetUserId());
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskRequest request)
        {
            var success = await _taskService.UpdateTaskAsync(id, request, GetUserId());
            if (success)
            {
                return Ok(new { message = "Operation successful" });
            }
            else
            {
                return NotFound(new { message = "Resource not found" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id, GetUserId());
            if (success)
            {
                return Ok(new { message = "Operation successful" });
            }
            else
            {
                return NotFound(new { message = "Record not found" });
            }
        }

        [Obsolete]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFilteredTasks(
    [FromQuery] bool? isCompleted,
    [FromQuery] string? priority,
    [FromQuery] DateTime? startDate,
    [FromQuery] DateTime? endDate,
    [FromQuery] string? search)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var tasks = await _taskService.GetFilteredTasksAsync(userId, isCompleted, priority, startDate, endDate, search);
            return Ok(tasks);
        }

    }
}
