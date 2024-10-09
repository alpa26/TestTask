using TestTask.Entities;
using TestTask.Entities.Interfaces;

namespace TestTask.Services.Interfaces
{
    public interface IGameRepository
    {
        public Task<List<Game>> FindListAsync();
        public Task<Game?> FindByIdAsync(Guid id);

    }
}
