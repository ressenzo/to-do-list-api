using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Domain.Entities;

public abstract class Entity : IEntity
{
    private const int ID_LENGTH = 8;
    private readonly List<string> _errors;

    protected Entity()
    {
        Id = Guid.NewGuid().ToString()[..ID_LENGTH];
        _errors = [];
    }

    protected Entity(string id)
    {
        Id = id;
        _errors = [];
    }

    public string Id { get; private set; }

    public IEnumerable<string> Errors =>
        _errors;

    public abstract bool IsValid();

    protected void AddError(string error) =>
        _errors.Add(error);
}