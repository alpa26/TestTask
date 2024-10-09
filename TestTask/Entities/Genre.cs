using System.ComponentModel.DataAnnotations;
using TestTask.Entities.Interfaces;

namespace TestTask.Entities
{
    public class Genre : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "null";
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
