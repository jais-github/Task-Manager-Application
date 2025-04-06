using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Features.Tasks
{
    public interface ITaskService
    {
        Task<List<TaskResponse>> GetTasksAsync(int userId);
        Task<TaskResponse?> GetTaskByIdAsync(int id, int userId);
        Task<TaskResponse> CreateTaskAsync(TaskRequest request, int userId);
        Task<bool> UpdateTaskAsync(int id, TaskRequest request, int userId);
        Task<bool> DeleteTaskAsync(int id, int userId);
        Task<List<TaskResponse>> GetFilteredTasksAsync(int userId, bool? isCompleted, string? priority, DateTime? startDate, DateTime? endDate, string? search);

    }
}
