using TaskManager.Features.Tags;

public interface ITagService
{
    Task<List<TagDto>> GetAllTagsAsync();
    Task<TagDto> CreateTagAsync(TagRequest request);
    Task<string> AssignTagToTaskAsync(int taskId, int tagId, int userId);
    Task<string> RemoveTagFromTaskAsync(int taskId, int tagId, int userId);
}
