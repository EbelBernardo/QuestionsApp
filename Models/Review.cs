using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Questions.Models;

[Table("reviews")]
public class Review : BaseModel
{
    [PrimaryKey("id", false)]
    [Column("id")]
    public Guid ID { get; set; }

    [Column("question_id")]
    public Guid QuestionID { get; set; }

    [Column("subject_id")]
    public Guid SubjectID { get; set; }

    [Column("profile_id")]
    public Guid ProfileID { get; set; }

    [Column("correct")]
    public bool Correct { get; set; }

    [Column("answered_at")]
    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

    [Column("rating")]
    public int? Rating { get; set; }
}