using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface ICreateTaskUseCase
{
    Task<Response<CreateTaskResponse>> CreateTask(string description);
}
