using Client_ui.Components.Authorization.Contracts;
using RegisterRequest = Client_ui.Components.Authorization.Contracts.RegisterRequest;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Client_ui.Persistance;
using Microsoft.EntityFrameworkCore;
using Client_ui.Service;
using Client_ui.Domain;
using System.Security.Claims;

public class AuthService
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
   /* public async Task<string> AuthenticationCheck(AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            return $"User is authenticated {user.Identity.Name}";
        }
        else
        {
            return "User is not authenticated";
        }
    }*/
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
    public void Logout()
    {
        _authenticationService.CurrentUser = new ClaimsPrincipal(new ClaimsIdentity());
    }
}