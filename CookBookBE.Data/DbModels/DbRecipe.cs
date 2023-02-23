using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CookBookBE.Data.DbModels
{
    public record DbRecipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public ICollection<DbIngredient> Ingredients { get; init; }
        public ICollection<DbTag> Tags { get; init; }

        [JsonIgnore]
        public DateTimeOffset DateCreated { get; init; }

        [JsonIgnore]
        public DateTimeOffset DateUpdated { get; init; }
    }
}
