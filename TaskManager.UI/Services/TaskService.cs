using System.Net.Http.Json;
using TaskManager.UI.Models;

namespace TaskManager.UI.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;

        public TaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TaskResponse>?> GetTasksAsync()
        {
            return await _http.GetFromJsonAsync<List<TaskResponse>>("api/tasks");
        }

        public async Task<TaskResponse?> GetTaskAsync(int id)
        {
            return await _http.GetFromJsonAsync<TaskResponse>($"api/tasks/{id}");
        }

        public async Task<bool> CreateTaskAsync(TaskRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/tasks", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskRequest request)
        {
            var response = await _http.PutAsJsonAsync($"api/tasks/{id}", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/tasks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
