namespace CookBookBE.Data.DbModels
{
    public record DbIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        // ilosc
        // jednostka
    }
}
