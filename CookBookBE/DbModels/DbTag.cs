namespace CookBookBE.DbModels
{
    public record DbTag
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
