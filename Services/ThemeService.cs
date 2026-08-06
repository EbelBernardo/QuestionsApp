using Microsoft.JSInterop;

namespace Perguntas.Client.Services;

public class ThemeService
{
    private readonly IJSRuntime _js;

    public ThemeService(IJSRuntime js)
    {
        _js = js;
    }


    public async Task SetTheme(string theme)
    {
        await _js.InvokeVoidAsync(
            "theme.set",
            theme
        );
    }


    public async Task<string> GetTheme()
    {
        return await _js.InvokeAsync<string>(
            "theme.get"
        );
    }
}