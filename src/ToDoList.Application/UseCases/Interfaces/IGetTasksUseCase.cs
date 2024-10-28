using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface IGetTasksUseCase
{
    Task<Response<GetTasksResponse>> GetTasks();
}
