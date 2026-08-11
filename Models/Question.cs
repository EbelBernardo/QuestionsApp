namespace Perguntas.Client.Models
{
    public class Question
    {
        public Guid ID { get; set; }
        public string Statement { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int TimesAnswered { get; set; }
        public int CorrectAnswers { get; set; }
        public bool LastAnswerCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastReview { get; set; }
        public Guid CategoryID { get; set; }
    }
}
