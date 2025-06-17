using System.Text;
using System.Text.Json;
using Client_ui.Domain.DTO;
using Client_ui.Service;
namespace Client_ui.Service
{

    public class RPComunnicationService : IRPComunnicationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RPComunnicationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://185.118.214.132:27017"; // PORT RPi
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<TrainingResponseDto> SendMessageAsync(string message, string userId = "blazor_user")
        {
            try
            {
                var request = new TrainingRequestDto
                {
                    message = message,
                    user_id = userId
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/chat", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TrainingResponseDto>(responseJson) ??
                           new TrainingResponseDto { response = "Pusta odpowiedź", success = false };
                }
                else
                {
                    return new TrainingResponseDto
                    {
                        response = $"Błąd HTTP: {response.StatusCode}",
                        success = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new TrainingResponseDto
                {
                    response = $"Błąd połączenia: {ex.Message}",
                    success = false
                };
            }
        }

        public async Task<UserStatsDto?> GetUserStatsAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/user/{userId}/stats");

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserStatsDto>(responseJson);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                // Test endpointu głównego 
                var response = await _httpClient.GetAsync($"{_baseUrl}/");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
