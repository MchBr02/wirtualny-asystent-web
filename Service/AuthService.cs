using Client_ui.Components.Authorization.Contracts;
using RegisterRequest = Client_ui.Components.Authorization.Contracts.RegisterRequest;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Client_ui.Persistance;
using Microsoft.EntityFrameworkCore;
using Client_ui.Service;
using Client_ui.Domain;
using System.Security.Claims;

public class AuthService : IAuthService
{
    private readonly WorkoutAppDbContext _dbContext;
    private readonly CustomAuthenticationService _authenticationService;

    public AuthService(
        WorkoutAppDbContext workoutAppDbContext,
        CustomAuthenticationService authenticationService)
    {
        _dbContext = workoutAppDbContext;
        _authenticationService = authenticationService;
    }

    public async Task<bool> Login(AuthRequest authRequest)
    {
        var user = await _dbContext.Users
            .Where(x => x.Username == authRequest.Username && x.PasswordHash == authRequest.Password)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        // Create claims for the authenticated user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            // Add more claims as needed, for example:
            // new Claim(ClaimTypes.Role, "User")
        };

        // Create ClaimsPrincipal
        var identity = new ClaimsIdentity(claims, "CustomAuth");
        var principal = new ClaimsPrincipal(identity);

        // Update the CustomAuthenticationService
        _authenticationService.CurrentUser = principal;
        return true;
    }

    public async Task<ClaimsPrincipal> GetCurrentUserAsync()
    {
        return _authenticationService.CurrentUser;
    }

    // New helper method to get current user ID directly
    public async Task<Guid?> GetCurrentUserIdAsync()
    {
        var currentUser = await GetCurrentUserAsync();
        if (currentUser?.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = currentUser.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
        }
        return null;
    }

    // Alternative helper method to check if user is authenticated
    public async Task<bool> IsUserAuthenticatedAsync()
    {
        var currentUser = await GetCurrentUserAsync();
        return currentUser?.Identity?.IsAuthenticated == true;
    }

    public void Logout()
    {
        _authenticationService.CurrentUser = new ClaimsPrincipal(new ClaimsIdentity());
    }
}