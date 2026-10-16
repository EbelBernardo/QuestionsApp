namespace Questions.Services
{
    public record CardState(
        double EaseFactor,
        int Repetitions,
        int IntervalDays,
        int Lapses,
        DateTime? DueAt
    );
    
}
