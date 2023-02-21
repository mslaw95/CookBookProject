using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBookBE.Data.DbModels
{
    public record DbRecipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public string Title { get; init; } = "";
        public string? Description { get; init; }
        public ICollection<DbIngredient>? Ingredients { get; init; }
        public List<DbTag>? Tags { get; init; }
        public DateTimeOffset CreatedDate { get; init; } = DateTimeOffset.Now;
    }
}
