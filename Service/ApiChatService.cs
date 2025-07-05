using Client_ui.Domain.DTO;
using System.Text.Json;
using System.Text;

namespace Client_ui.Service
{
    public class ApiChatService : IApiChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiChatService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Różne opcje w zależności od konfiguracji Docker
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? DetermineApiUrl();
        }

        private string DetermineApiUrl()
        {
            // Sprawdź czy aplikacja działa w kontenerze
            var dockerEnv = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");

            if (!string.IsNullOrEmpty(dockerEnv))
            {
                // W kontenerze - użyj nazwy serwisu Docker lub host.docker.internal
                return "http://host.docker.internal:8081"; // Poprawiony port na 8081
                // Alternatywnie: return "http://api-service:8081"; // Jeśli używasz docker-compose z nazwą serwisu
            }

            // Lokalnie - użyj localhost
            return "http://localhost:8081"; // Poprawiony port na 8081
        }

        public async Task<ApiResponseDto> SendMessageAsync(string message, string? userId = null)
        {
            try
            {
                var requestBody = new
                {
                    message = message,
                    userId = userId
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/message", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<ApiResponseDto>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse ?? new ApiResponseDto { Success = false, Response = "Brak odpowiedzi z serwera" };
                }
                else
                {
                    return new ApiResponseDto
                    {
                        Success = false,
                        Response = $"Błąd HTTP: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponseDto
                {
                    Success = false,
                    Response = $"Błąd połączenia: {ex.Message}"
                };
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                // Wyślij testowe zapytanie POST zamiast GET
                var testMessage = new
                {
                    message = "test connection",
                    userId = "test"
                };

                var json = JsonSerializer.Serialize(testMessage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/message", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}