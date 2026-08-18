using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Perguntas.Client.Services;

public class CustomAuthenticationStateProvider
    : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CustomAuthenticationStateProvider(
        AuthService authService)
    {
        _authService = authService;

        _authService.AuthenticationStateChanged +=
            OnAuthenticationStateChanged;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userId = _authService.CurrentUserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            var anonymous = new ClaimsPrincipal(
                new ClaimsIdentity());

            return Task.FromResult(
                new AuthenticationState(anonymous));
        }

        var identity = new ClaimsIdentity(
            new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    userId)
            },
            authenticationType: "Supabase");

        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(
            new AuthenticationState(user));
    }

    private void OnAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(
            GetAuthenticationStateAsync());
    }
}