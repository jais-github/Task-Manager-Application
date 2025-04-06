namespace TaskManager.UI.Models
{
    public class TaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? Priority { get; set; }
    }
}
