using Questions.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Questions.Services
{
    public class StudyService
    {
        private readonly ReviewService _reviewService;
        private readonly QuestionService _questionService;

        public StudyService(ReviewService reviewService, QuestionService questionService)
        {
            _reviewService = reviewService;
            _questionService = questionService;
        }

        public async Task AnswerAsync(Question question, Rating rating)
        {
            var utcNow = DateTime.UtcNow;
            var correct = rating != Rating.Errei;

            // Create Review 
            await _reviewService.CreateAsync(new Review
            {
                QuestionID = question.ID,
                SubjectID = question.SubjectID,
                Correct = correct,
                Rating = (int)rating,
            });

            // Update Question
            var next = SpacedRepetition.Next(question.ToCardState(), rating, utcNow);
            next.ApplyTo(question);

            // Update old fileds
            question.TimesAnswered++;
            question.LastAnswerCorrect = correct;
            if (correct) question.CorrectAnswers++;
            question.LastReview = utcNow;

            // Save Question
            await _questionService.UpdateAsync(question);

        }
    }
}
