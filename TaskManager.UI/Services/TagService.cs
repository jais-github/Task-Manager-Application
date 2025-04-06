using System.Net.Http.Json;
using TaskManager.UI.Models;

namespace TaskManager.UI.Services
{
    public class TagService
    {
        private readonly HttpClient _http;

        public TagService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TagResponse>?> GetTagsAsync()
        {
            return await _http.GetFromJsonAsync<List<TagResponse>>("api/tags");
        }

        public async Task<bool> CreateTagAsync(TagRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/tags", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AssignTagToTaskAsync(int taskId, int tagId)
        {
            var response = await _http.PostAsync($"api/tags/assign?taskId={taskId}&tagId={tagId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveTagFromTaskAsync(int taskId, int tagId)
        {
            var response = await _http.PostAsync($"api/tags/remove?taskId={taskId}&tagId={tagId}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
