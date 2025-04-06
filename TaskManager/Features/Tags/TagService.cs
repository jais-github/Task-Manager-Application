using TaskManager.API.Data;
using TaskManager.Features.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Features.Tags;

public class TagService : ITagService
{
    private readonly AppDbContext _context;

    public TagService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TagDto>> GetAllTagsAsync()
    {
        var tags = await _context.Tags.ToListAsync();
        return tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList();
    }

    public async Task<TagDto> CreateTagAsync(TagRequest request)
    {
        var tag = new Tag { Name = request.Name };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return new TagDto { Id = tag.Id, Name = tag.Name };
    }

    public async Task<string> AssignTagToTaskAsync(int taskId, int tagId, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        var tag = await _context.Tags.FindAsync(tagId);

        if (task != null && tag != null && !task.Tags.Any(t => t.Id == tagId))
        {
            task.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return "success";
        }

        return "Tag already Assigned";
    }

    public async Task<string> RemoveTagFromTaskAsync(int taskId, int tagId, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        var tag = await _context.Tags.FindAsync(tagId);

        if (task != null && tag != null && task.Tags.Any(t => t.Id == tagId))
        {
            task.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return "success";
        }
        return "Tag Not Assigned";
    }
}
