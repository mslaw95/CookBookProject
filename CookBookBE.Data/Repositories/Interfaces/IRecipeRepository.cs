using CookBookBE.Data.DbModels;

namespace CookBookBE.Data.Repositories.Interfaces
{
    public interface IRecipeRepository : IBaseContextRepository<DbRecipe>
    {
        Task PopulateDbWithData();
    }
}
