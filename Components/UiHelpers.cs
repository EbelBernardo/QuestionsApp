namespace Questions.Components;

/// <summary>
/// ══════════════════════════════════════════════════════════════════════════
/// UiHelpers — ARQUIVO NOVO, 100% APRESENTAÇÃO
/// ──────────────────────────────────────────────────────────────────────────
/// Só existe para não repetir as mesmas 5 linhas em três páginas.
/// NÃO contém regra de negócio, NÃO acessa serviços, NÃO acessa o Supabase e
/// NÃO calcula estatística nenhuma. É apenas:
///   · iniciais de um nome  → marca visual da matéria
///   · cor de identificação → qual das 6 cores da paleta cabe a cada matéria
///
/// Se você preferir não ter este arquivo, é só colar os dois métodos como
/// `private static` dentro de cada página.
/// ══════════════════════════════════════════════════════════════════════════
/// </summary>
public static class UiHelpers
{
    /// <summary>
    /// Quantidade de cores de matéria disponíveis em theme.css
    /// (--qa-subj-1 … --qa-subj-6).
    /// </summary>
    public const int SubjectColorCount = 6;

    /// <summary>
    /// Iniciais para a marca da matéria ou do usuário.
    /// "Direito Constitucional" → "DC" · "Farmacologia" → "FA"
    /// </summary>
    public static string Initials(string? name, int max = 2)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "?";

        var words = name
            .Split(new[] { ' ', '-', '—', '–', '_', '/' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(w => char.IsLetterOrDigit(w[0]))
            .ToArray();

        if (words.Length == 0)
            return "?";

        if (words.Length == 1)
        {
            var single = words[0];
            return (single.Length >= max ? single[..max] : single).ToUpperInvariant();
        }

        return string.Concat(words.Take(max).Select(w => w[0])).ToUpperInvariant();
    }

    /// <summary>
    /// Índice de cor (1..6) estável para um Guid.
    /// Usa os bytes do Guid — o mesmo id sempre recebe a mesma cor, em
    /// qualquer execução e em qualquer máquina.
    /// </summary>
    public static int SubjectColorIndex(Guid id)
    {
        var bytes = id.ToByteArray();

        var sum = 0;

        foreach (var b in bytes)
            sum += b;

        return (sum % SubjectColorCount) + 1;
    }

    /// <summary>
    /// Valor pronto para o atributo style de um elemento, definindo a variável
    /// CSS que todo o design system consome como "cor desta matéria".
    ///
    /// Uso:  &lt;article class="qa-tile" style="@UiHelpers.SubjectStyle(c.ID)"&gt;
    /// </summary>
    public static string SubjectStyle(Guid id)
        => $"--qa-subject: var(--qa-subj-{SubjectColorIndex(id)});";
}
