namespace CookBookBE.Data.Models
{
    public record Recipe
    {
        public string Title { get; init; }
        public string? Description { get; init; }
        public ICollection<Ingredient> Ingredients { get; init; }
        public ICollection<Tag>? Tags { get; init; }
    }
}
