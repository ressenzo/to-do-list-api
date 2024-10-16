using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Domain.Factories.Interfaces;

public interface ITaskFactory
{
    ITask Factory(string description);
}
