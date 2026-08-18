using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Perguntas.Client.Services;
using Questions;
using Questions.Config;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<SupabaseService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<
    AuthenticationStateProvider,
    CustomAuthenticationStateProvider>();

builder.Services.AddSingleton<SupabaseSessionHandler>();

builder.Services.AddSingleton<Supabase.Client>(sp =>
{
    var handler = sp.GetRequiredService<SupabaseSessionHandler>();

    return new Supabase.Client(
        SupabaseConfig.Url,
        SupabaseConfig.Key,
        new SupabaseOptions
        {
            AutoRefreshToken = true,
            SessionHandler = handler
        });
});

var host = builder.Build();

var supabase = host.Services.GetRequiredService<Supabase.Client>();

await supabase.InitializeAsync();

supabase.Auth.LoadSession();

await supabase.Auth.RetrieveSessionAsync();

await host.RunAsync();