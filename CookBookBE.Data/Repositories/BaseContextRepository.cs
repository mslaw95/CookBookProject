using CookBookBE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBookBE.Data.Repositories
{
    public class BaseContextRepository<T> : IBaseContextRepository<T> where T : class
    {
        internal readonly IDbContextFactory<RecipeContext> _dbContextFactory;

        public BaseContextRepository(IDbContextFactory<RecipeContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task AddAsync(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Set<T>().FindAsync(id);
            // If T not of nullable type, do sth?
        }

        public async Task UpdateAsync(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext();
            // context.ChangeTracker.Clear();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
