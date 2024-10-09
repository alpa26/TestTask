using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTask.Entities.Interfaces;

namespace TestTask.Entities
{
    public class Game : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "null";
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public Guid DeveloperId { get; set; }

        [NotMapped]
        public Developer Developer { get; set; }
    }
}
