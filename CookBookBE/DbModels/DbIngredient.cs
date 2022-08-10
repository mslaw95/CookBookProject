namespace CookBookBE.DbModels
{
    public record DbIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
