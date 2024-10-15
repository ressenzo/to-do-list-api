using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Domain.Entities;

public abstract class Entity : IEntity
{
    private readonly List<string> _errors;

    protected Entity()
    {
        Id = Guid.NewGuid();
        _errors = [];
    }

    public Guid Id { get; private set; }

    public IEnumerable<string> Errors =>
        _errors;

    public abstract bool IsValid();

    protected void AddError(string error) =>
        _errors.Add(error);
}