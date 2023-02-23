namespace CookBookBE.Data.Models
{
    public record Ingredient
    {
        public string Name { get; set; }
        public uint Amount { get; set; }
        public string Unit { get; set; }
    }
}
