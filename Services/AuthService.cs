using Questions.Models;
using Supabase;
using Supabase.Gotrue;

namespace Perguntas.Client.Services;

public class AuthService
{
    private readonly Supabase.Client _supabase;

    public event Action? AuthenticationStateChanged;

    public AuthService(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public bool IsAuthenticated =>
        _supabase.Auth.CurrentUser != null;

    public string? CurrentUserId =>
        _supabase.Auth.CurrentUser?.Id;

    public async Task<bool> RegisterAsync(
        string name,
        string email,
        string password)
    {
        var options = new SignUpOptions
        {
            Data = new Dictionary<string, object>
            {
                ["name"] = name
            }
        };

        var session = await _supabase.Auth.SignUp(
            email,
            password,
            options);

        if (session?.User == null)
            return false;

        AuthenticationStateChanged?.Invoke();

        return true;
    }

    public async Task<bool> LoginAsync(
        string email,
        string password)
    {
        var session = await _supabase.Auth.SignIn(
            email,
            password);

        if (session?.User == null)
            return false;

        Console.WriteLine($"LOGIN USER: {session.User.Id}");
        Console.WriteLine($"CURRENT USER: {_supabase.Auth.CurrentUser?.Id}");

        AuthenticationStateChanged?.Invoke();

        return true;
    }

    public async Task LogoutAsync()
    {
        await _supabase.Auth.SignOut();

        AuthenticationStateChanged?.Invoke();
    }

    public async Task<bool> HandleAuthenticationExceptionAsync(Exception ex)
    {
        if (ex is not Supabase.Postgrest.Exceptions.PostgrestException postgrestException)
            return false;

        if (postgrestException.Message.Contains("JWT expired") ||
            postgrestException.Message.Contains("JWT issued at future") ||
            postgrestException.Message.Contains("\"code\":\"PGRST303\""))
        {
            await LogoutAsync();

            return true;
        }

        return false;
    }

    public async Task<T?> ExecuteAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            if (await HandleAuthenticationExceptionAsync(ex))
                return default;

            throw;
        }
    }

    public async Task<Profile?> GetCurrentProfileAsync()
    {
        if (!IsAuthenticated)
            return null;

        var userId = Guid.Parse(CurrentUserId!);

        return await ExecuteAsync(async () =>
        {
            var response = await _supabase
                .From<Profile>()
                .Where(p => p.Id == userId)
                .Single();

            return response;
        });
    }

}