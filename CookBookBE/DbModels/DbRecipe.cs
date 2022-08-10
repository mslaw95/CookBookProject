using CookBookBE.DbModels;

namespace CookBookBE.Models
{
    public record DbRecipe
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = "";
        public string? Description { get; init; }
        public List<DbIngredient>? Ingredients { get; init; }
        public List<DbTag>? Tags { get; init; }
        public DateTimeOffset CreatedDate { get; init; } = DateTimeOffset.Now;
    }
}
