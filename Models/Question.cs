using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Perguntas.Client.Models
{
    [Table("questions")]
    public class Question : BaseModel
    {
        [PrimaryKey("id", false)]
        [Column("id")]
        public Guid ID { get; set; }
        [Column("statement")]
        public string Statement { get; set; } = string.Empty;
        [Column("answer")]
        public string Answer { get; set; } = string.Empty;
        [Column("times_answered")]
        public int TimesAnswered { get; set; }
        [Column("correct_answers")]
        public int CorrectAnswers { get; set; }
        [Column("last_answer_correct")]
        public bool LastAnswerCorrect { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("last_review")]
        public DateTime? LastReview { get; set; }
        [Column("category_id")]
        public Guid CategoryID { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}
