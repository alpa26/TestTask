using System.ComponentModel.DataAnnotations;
using TestTask.Entities.Interfaces;

namespace TestTask.Entities
{
    public class Developer: IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "null";

    }
}
