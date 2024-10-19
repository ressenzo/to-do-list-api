using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Infrastructure.Repositories.Interfaces;

public interface ITaskRepository
{
    Task CreateTask(ITask task);
}
