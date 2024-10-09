using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestTask.Data;
using TestTask.Entities;
using TestTask.Entities.Interfaces;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class GameRepository: IGameRepository
    {
        private readonly AppDbContext _database;

        public GameRepository(AppDbContext database)
        {
            _database = database;
        }

        public async Task<List<Game>> FindListAsync() 
        {

            var list = await _database.Games.Include(x => x.Genres).Include(x=>x.Developer).ToListAsync();
            foreach (var game in list)
                foreach (var item in game.Genres)
                    item.Games.Clear();
            return list;
        }

        public async Task<Game?> FindByIdAsync(Guid id)
        {
            var res = await _database.Games.Include(x => x.Genres).Include(x => x.Developer).FirstOrDefaultAsync(x => x.Id == id);
            foreach (var item in res.Genres)
                    item.Games.Clear();
            return res; 
        }
    }
}
