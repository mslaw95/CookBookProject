namespace CookBookBE.Models
{
    public record Recipe
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = "";
        public string? Description { get; init; }
        public List<Ingredient>? Ingredients { get; init; }
        public List<Tag>? Tags { get; init; }
    }
}
