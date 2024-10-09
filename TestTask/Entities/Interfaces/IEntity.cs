namespace TestTask.Entities.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        string Name { get; set; }

    }
}
