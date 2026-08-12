using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Perguntas.Client.Services;
using Questions;
using Supabase;
using Questions.Config;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<SupabaseService>();

var supabase = new Supabase.Client(
    SupabaseConfig.Url,
    SupabaseConfig.Key
);

await supabase.InitializeAsync();

builder.Services.AddSingleton(supabase);

await builder.Build().RunAsync();
