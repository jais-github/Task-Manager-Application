using Azure;
using TaskManager.API.Features.Auth;
using TaskManager.Features.Tags;

namespace TaskManager.Features.Tasks
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? Priority { get; set; } // Low, Medium, High

        // Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Tag> Tags { get; set; } = new();
    }
}
