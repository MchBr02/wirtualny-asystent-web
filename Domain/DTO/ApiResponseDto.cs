namespace Client_ui.Domain.DTO
{
    public class ApiResponseDto
    {
        public bool Success { get; set; } = true;
        public string Response { get; set; } = string.Empty;
        public string? Error { get; set; }
    }
}