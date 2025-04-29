using Blazored.SessionStorage;
using Client_ui.Components.Authorization.Contracts;
using Client_ui.Service;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client_ui.Components.Authorization
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState authenticationState;

        public CustomAuthStateProvider(CustomAuthenticationService service)
        {
            authenticationState = new AuthenticationState(service.CurrentUser);

            service.UserChanged += (newUser) =>
            {
                authenticationState = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            };
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(authenticationState);
    }
}
