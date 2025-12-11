namespace CookBookBE.Api.Services.Interfaces
{
    public interface IStorageService
    {
        Task AddFileToRecipeAsync(Guid recipeId, string filePath);
    }
}
