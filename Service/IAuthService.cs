using Client_ui.Components.Authorization.Contracts;
using System.Security.Claims;

public interface IAuthService
{
    Task<bool> Login(AuthRequest authRequest);
    Task<ClaimsPrincipal> GetCurrentUserAsync();
    Task<Guid?> GetCurrentUserIdAsync(); 
    Task<bool> IsUserAuthenticatedAsync();
    void Logout();
}