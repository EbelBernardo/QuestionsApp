using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Perguntas.Client.Models
{
    [Table("categories")]
    public class Category : BaseModel
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
