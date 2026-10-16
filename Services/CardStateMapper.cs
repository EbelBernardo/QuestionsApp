using Questions.Models;
using System.Runtime.CompilerServices;

namespace Questions.Services
{
    public static class CardStateMapper
    {
        public static CardState ToCardState(this Question question)
            => new CardState(
                    question.EaseFactor,
                    question.Repetitions,
                    question.IntervalDays,
                    question.Lapses,
                    question.DueAt);

        public static void ApplyTo(this CardState cardState, Question question)
        {
            question.EaseFactor = cardState.EaseFactor;
            question.Repetitions = cardState.Repetitions;
            question.IntervalDays = cardState.IntervalDays;
            question.Lapses = cardState.Lapses;
            question.DueAt = cardState.DueAt;
        }

        
    }
}
