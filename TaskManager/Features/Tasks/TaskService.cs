using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.Features.Tags;

namespace TaskManager.Features.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskResponse>> GetTasksAsync(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Tags)
                .ToListAsync();

            return tasks.Select(ToTaskResponse).ToList();
        }

        public async Task<TaskResponse?> GetTaskByIdAsync(int id, int userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            return task == null ? null : ToTaskResponse(task);
        }

        public async Task<TaskResponse> CreateTaskAsync(TaskRequest request, int userId)
        {
            var tags = await GetOrCreateTagsAsync(request.Tags);

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                UserId = userId,
                Tags = tags
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return ToTaskResponse(task);
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskRequest request, int userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return false;

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.Priority = request.Priority;
            task.IsCompleted = request.IsCompleted;

            task.Tags = await GetOrCreateTagsAsync(request.Tags);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id, int userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskResponse>> GetFilteredTasksAsync(
            int userId,
            bool? isCompleted,
            string? priority,
            DateTime? startDate,
            DateTime? endDate,
            string? search)
        {
            var query = _context.Tasks
                .Where(t => t.UserId == userId)
                .AsQueryable();

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(t => t.Priority != null && t.Priority.ToLower() == priority.ToLower());

            if (startDate.HasValue)
                query = query.Where(t => t.DueDate.HasValue && t.DueDate.Value >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.DueDate.HasValue && t.DueDate.Value <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Title.Contains(search) || (t.Description != null && t.Description.Contains(search)));

            var tasks = await query
                .Include(t => t.Tags)
                .ToListAsync();

            return tasks.Select(ToTaskResponse).ToList();
        }

        // 🔁 Helper: Convert Entity to Response DTO
        private TaskResponse ToTaskResponse(TaskItem task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority,
                Tags = task.Tags.Select(tag => tag.Name).ToList()
            };
        }

        // 🔁 Helper: Create or reuse existing tags
        private async Task<List<Tag>> GetOrCreateTagsAsync(List<string>? tagNames)
        {
            var tags = new List<Tag>();

            if (tagNames == null || !tagNames.Any())
                return tags;

            foreach (var name in tagNames.Distinct())
            {
                var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);

                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
                else
                {
                    var newTag = new Tag { Name = name };
                    _context.Tags.Add(newTag);
                    tags.Add(newTag);
                }
            }

            return tags;
        }
    }
}
