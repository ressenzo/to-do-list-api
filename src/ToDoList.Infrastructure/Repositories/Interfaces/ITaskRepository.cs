using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Infrastructure.Repositories.Interfaces;

public interface ITaskRepository
{
    Task CreateTask(ITask task);

    Task<ITask?> GetTask(string id);

    Task<bool> UpdateTask(ITask task);

    Task<IEnumerable<ITask>?> GetTasks();
}
