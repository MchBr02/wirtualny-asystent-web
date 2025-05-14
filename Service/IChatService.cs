namespace Client_ui.Service
{
    public interface IChatService
    {
        Task<string> TestPOST();
        Task<string> TestGET();
        Task<string> SendCustomMessage(string message);
    }
}
