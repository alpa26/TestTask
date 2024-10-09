using Microsoft.AspNetCore.Mvc;
using TestTask.Entities;
using TestTask.Services;
using TestTask.Services.Interfaces;


namespace TestTask.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {

        public readonly Repository _repository;
        public readonly GameRepository _gamerepository;


        public GameController(Repository repository, GameRepository gamerepository) {
            _repository = repository;
            _gamerepository = gamerepository;
        }

        // POST api/<GameController>
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Game game)
        {
            var dev = await _repository.FindByNameAsync<Developer>(game.Developer.Name);
            if (dev == null) {
                dev = new Developer { Id = Guid.NewGuid(), Name = game.Developer.Name };
                await _repository.CreateAsync(dev);
            }
            game.Id = Guid.NewGuid();
            game.DeveloperId = dev.Id;
            game.Developer = dev;

            for (var i = 0; i < game.Genres.Count; i++)
            {
                var genre = await _repository.FindByNameAsync<Genre>(game.Genres[i].Name);
                if (genre == null)
                    return BadRequest("Genre does not exist");
                genre.Games.Clear();
                game.Genres.Add(genre);
            }


            var res = await _repository.CreateAsync(game);
            if (res != Guid.Empty)
                return Ok();
            else
                return BadRequest();
        }

        // GET: api/<GameController>
        [HttpGet("getlist")]
        public async Task<IList<Game>> GetList()
        {
            return await _gamerepository.FindListAsync();
        }

        [HttpGet("getlistbygenre/{genre}")]
        public async Task<IList<Game>> GetListByGenre(string genre)
        {
            var res = new List<Game>();
            var list = await _gamerepository.FindListAsync();
            foreach (var item in list)
            {
                foreach (var gen1 in item.Genres)
                    if (gen1.Name.Equals(genre))
                        res.Add(item);
            }
            return res;
        }

        // PUT api/<GameController>/5
        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] Game game)
        {
            var changegame = await _gamerepository.FindByIdAsync(game.Id);
            changegame.Name = game.Name;

            var dev = await _repository.FindByNameAsync<Developer>(game.Developer.Name);
            if (dev == null)
            {
                dev = new Developer { Id = Guid.NewGuid(), Name = game.Developer.Name };
                await _repository.CreateAsync(dev);
            }
            changegame.Developer = dev;
            changegame.DeveloperId = changegame.Developer.Id;

            for (var i = 0; i < game.Genres.Count; i++)
            {
                var genre = await _repository.FindByNameAsync<Genre>(game.Genres[i].Name);
                if (genre == null)
                    return BadRequest("Genre does not exist");
                changegame.Genres.Add(genre);
            }

            var res = await _repository.ChangeAsync(changegame);
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        // DELETE api/<GameController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _repository.RemoveAsync<Game>(id);
            if (res)
                return Ok();
            else
                return BadRequest();
        }
    }
}
