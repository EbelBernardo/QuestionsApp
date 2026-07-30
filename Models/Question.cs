namespace Perguntas.Client.Models
{
    public class Question
    {
        public Guid ID { get; set; }
        public string Statement { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public Guid CategoryID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastReview { get; set; }
    }
}
