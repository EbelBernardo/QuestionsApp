using System.Security.Cryptography.X509Certificates;
using Questions.Models;

namespace Questions.Services;

public enum Rating
{
    Errei = 1,
    Dificil = 2,
    Bom = 3,
    Facil = 4
}

public class SpacedRepetition
{
    public static CardState Next(CardState current, Rating rating, DateTime utcNow)
    {
        var easeFactor = current.EaseFactor;
        var repetitions = current.Repetitions;
        var intervalDays = current.IntervalDays;
        var lapses = current.Lapses;

        var q = ToQuality((Rating)rating);

        if (q >= 3)
        {
            intervalDays = repetitions switch
            {
                0 => 1,
                1 => 6,
                _ => (int)Math.Round(intervalDays * easeFactor, MidpointRounding.AwayFromZero)
            };

            repetitions++;
        }
        else
        {
            repetitions = 0;
            intervalDays = 1;
            lapses++;
        }

        easeFactor = CalculateEaseFactor(easeFactor, q);

        return current with
        {
            EaseFactor = easeFactor,
            Repetitions = repetitions,
            IntervalDays = intervalDays,
            Lapses = lapses,
            DueAt = utcNow.AddDays(intervalDays)
        };
    }

    private static double CalculateEaseFactor(double easeFactor, int q)
        => Math.Max(1.3, easeFactor + 0.1 - (5 - q) * (0.08 + (5 - q) * 0.02));

    private static int ToQuality(Rating rating) => rating switch
    {
        Rating.Errei => 0,
        Rating.Dificil => 3,
        Rating.Bom => 4,
        Rating.Facil => 5,
        _ => throw new ArgumentOutOfRangeException(nameof(rating))
    };
}