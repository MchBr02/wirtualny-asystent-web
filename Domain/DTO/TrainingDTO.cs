namespace Client_ui.Domain.DTO
{
    public class TrainingRequestDto
    {
        public string message { get; set; }
        public string user_id { get; set; } = "blazor_user";
    }

    public class TrainingResponseDto
    {
        public string response { get; set; }
        public bool success { get; set; } = true;
    }

    public class UserStatsDto
    {
        public string user_id { get; set; }
        public List<object> trainings { get; set; }
        public List<object> exercises { get; set; }
    }
}
