using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Entities;
using TestTask.Services;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        public readonly Repository _repository;
        public GenreController(Repository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Genre genre)
        {
            var gen = await _repository.FindByNameAsync<Genre>(genre.Name);
            if (gen != null)
            { 
                return BadRequest("Already exist");
            }
            genre.Id = Guid.NewGuid();
            var res = await _repository.CreateAsync(genre);
            if (res != Guid.Empty)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("getlist")]
        public async Task<IList<Genre>> GetList()
        {
            return await _repository.FindListAsync<Genre>();
        }
    }
}
