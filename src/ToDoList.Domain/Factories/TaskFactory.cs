using Task = ToDoList.Domain.Entities.Task;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Domain.Factories.Interfaces;

namespace ToDoList.Domain.Factories;

public class TaskFactory : ITaskFactory
{
    public ITask Factory(string description) =>
        Task.Construct(description);
}
