using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.Controllers
{
    [Route("api/developers")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        public readonly Repository _repository;
        public DeveloperController(Repository repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Developer developer)
        {
            var gen = await _repository.FindByNameAsync<Developer>(developer.Name);
            if (gen != null)
            {
                return BadRequest("Already exist");
            }
            developer.Id = Guid.NewGuid();
            var res = await _repository.CreateAsync(developer);
            if (res != Guid.Empty)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("getlist")]
        public async Task<IList<Developer>> GetList()
        {
            return await _repository.FindListAsync<Developer>();
        }
    }
}
