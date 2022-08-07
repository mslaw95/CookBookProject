namespace CookBookBE.Models
{
    public record Recipe
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
