using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.API.Features.Auth;
using TaskManager.Features.Tags;

[ApiController]
[Route("api/tags")]
[Authorize]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        var tags = await _tagService.GetAllTagsAsync();
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] TagRequest request)
    {
        var tag = await _tagService.CreateTagAsync(request);
        return Ok(tag);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignTag(int taskId, int tagId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _tagService.AssignTagToTaskAsync(taskId, tagId, userId);
        if (result == "success")
        {
            return Ok(new { message = "success" });
        }
        else
        {
            return NotFound(new { message = "Tag Already Assigned" });
        }
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveTag(int taskId, int tagId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _tagService.RemoveTagFromTaskAsync(taskId, tagId, userId);

        if(result == "success")
        {
            return Ok(new { message = "success" });
        }
        else
        {
            return NotFound(new { message = "Tag Not Assigned" });
        }
    }
}
