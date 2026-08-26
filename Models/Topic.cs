using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Questions.Models;

[Table("topics")]
public class Topic : BaseModel
{
    [PrimaryKey("id", false)]
    [Column("id")]
    public Guid ID { get; set; }

    [Column("subject_id")]
    public Guid SubjectID { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("position")]
    public int Position { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}