using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities.Interfaces;

public interface ITask : IEntity
{
    public string Description { get; }

    public DateTime CreationDate { get; }

    public Status Status { get; }

    public void SetAsInProgress();

    public void SetAsDone();
    
    public void SetAsCanceled();
}