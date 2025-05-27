using Client_ui.Domain.DTO;
using Client_ui.Service;
using Microsoft.AspNetCore.Mvc;
using Client_ui.Domain.DTO;

namespace Client_ui.Controllers
{
    [ApiController]
    [Route("api/[controller]")]        // <-- tu
    public class ChatController : ControllerBase
    {
        private readonly CustomAuthenticationService _customAuthenticationService;
        private readonly IChatService _chatService;
        public ChatController(CustomAuthenticationService customAuthenticationService, IChatService chatService)
        {
            _customAuthenticationService = customAuthenticationService;
            _chatService = chatService;
        }
    }
}
