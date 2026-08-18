using Microsoft.JSInterop;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;

namespace Perguntas.Client.Services;

public class SupabaseSessionHandler : IGotrueSessionPersistence<Session>
{
    private readonly IJSInProcessRuntime _js;

    public SupabaseSessionHandler(IJSRuntime js)
    {
        _js = (IJSInProcessRuntime)js;
    }

    public void SaveSession(Session session)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(session);

        _js.InvokeVoid(
            "supabaseSession.save",
            json);
    }

    public Session? LoadSession()
    {
        var json = _js.Invoke<string?>(
            "supabaseSession.load");

        if (string.IsNullOrWhiteSpace(json))
            return null;

        return System.Text.Json.JsonSerializer.Deserialize<Session>(json);
    }

    public void DestroySession()
    {
        _js.InvokeVoid(
            "supabaseSession.remove");
    }
}