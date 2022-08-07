namespace CookBookBE.Models
{
    public record DbRecipe
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
