﻿using TaskManager.Features.Tasks;

namespace TaskManager.Features.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
