namespace CookBookBE.Data.Models
{
    public record Recipe
    {
        // TODO Should this model have any unique identifier?
        public string Title { get; init; } = "";
        public string? Description { get; init; }
        public List<Ingredient>? Ingredients { get; init; }
        public List<Tag>? Tags { get; init; }
    }
}
