using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Questions.Models
{
    [Table("subjects")]
    public class Subject : BaseModel
    {
        [PrimaryKey("id", false)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}
