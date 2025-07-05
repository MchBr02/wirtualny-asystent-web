namespace Client_ui.Domain.DTO
{
    public class ChatMessageDto
    {
        public string Content { get; set; } = string.Empty;
        public bool IsUser { get; set; }
        public DateTime Timestamp { get; set; }
    }
}