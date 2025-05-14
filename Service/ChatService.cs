using Client_ui.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client_ui.Service
{
 
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<string> TestGET()
        {
            var res = await _httpClient.GetAsync("api/security/test");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> TestPOST()
        {
            try
            {
                var dto = new MessageDTO { Message = "Hello from Blazor" };

                Console.WriteLine($"Sending POST to: {_httpClient.BaseAddress}api/chat/receive");

                var res = await _httpClient.PostAsJsonAsync("api/chat/receive", dto);
                Console.WriteLine($"Response Status: {res.StatusCode}");

                res.EnsureSuccessStatusCode();

                var content = await res.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {content}");

                // Deserialize and format nicely for display
                var responseObj = JsonSerializer.Deserialize<MessageResponse>(content, _jsonOptions);
                return $"Serwer otrzymał: {responseObj?.Received}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in TestPOST: {ex.Message}");
                throw;
            }
        }

        public async Task<string> SendCustomMessage(string message)
        {
            try
            {
                var dto = new MessageDTO { Message = message };

                var res = await _httpClient.PostAsJsonAsync("api/chat/receive", dto);
                res.EnsureSuccessStatusCode();

                var content = await res.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<MessageResponse>(content, _jsonOptions);

                return $"Serwer otrzymał: {responseObj?.Received}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in SendCustomMessage: {ex.Message}");
                throw;
            }
        }
    }

    // Klasa odpowiedzi z serwera
    public class MessageResponse
    {
        public string? Received { get; set; }
    }
}