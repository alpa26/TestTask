using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Entities.Interfaces;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _database;

        public Repository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<Guid?> CreateAsync<T>(T item) where T : class, IEntity
        {
            try
            {
                var result = await GetCollection<T>().AddAsync(item);

                if (result.State != EntityState.Added)
                    return null;
                await SaveChangesAsync();
                return result.Entity.Id;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> ChangeAsync<T>(T item)where T : class, IEntity
        {
            var existingItem = await GetCollection<T>().FindAsync(item.Id);
            if (existingItem == null)
                return false;
            try
            {
                GetCollection<T>().Update(item);
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<T?> FindByIdAsync<T>(Guid id) where T : class, IEntity
        {
            return await GetCollection<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> FindByNameAsync<T>(string name) where T : class, IEntity
        {
            return await GetCollection<T>().FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task<List<T>> FindListAsync<T>() where T : class, IEntity
        {
            return await GetCollection<T>().ToListAsync();
        }

        public async Task<bool> RemoveAsync<T>(Guid? id) where T : class, IEntity
        {
            var entity = await GetCollection<T>().FindAsync(id);
            if (entity == null)
                return false;

            GetCollection<T>().Remove(entity);
            try
            {
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private DbSet<T?> GetCollection<T>() where T : class, IEntity
        {
            var property = _database.GetType().GetProperties()
                            .FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));

            if (property != null)
            {
                return (DbSet<T>)property.GetValue(_database);
            }
            throw new ArgumentException($"DBSet<{typeof(T).Name}> not found in the AppDbContext");
        }
    }
}
