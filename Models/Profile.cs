using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Questions.Models;

[Table("profiles")]
public class Profile : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}