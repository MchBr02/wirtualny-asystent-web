namespace Client_ui.Domain.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public Guid WorkoutId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
