namespace TaskManager.UI.Models
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? Priority { get; set; }

        public List<TagResponse> Tags { get; set; } = new();
    }
}
