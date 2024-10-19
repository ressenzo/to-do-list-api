using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

internal class TaskRepository : ITaskRepository
{
    public Task CreateTask(ITask task)
    {
        throw new NotImplementedException();
    }
}
