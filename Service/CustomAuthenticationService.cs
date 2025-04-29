using Client_ui.Components.Authorization.Contracts;
using System.Security.Claims;

namespace Client_ui.Service
{
    public class CustomAuthenticationService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal currentUser;
        
        public ClaimsPrincipal CurrentUser
        {
            get { return currentUser ?? new(); }
            set
            {
                currentUser = value;
                if (UserChanged is not null)
                {
                    UserChanged.Invoke(currentUser);
                }
            }
        }
        
    }
}
