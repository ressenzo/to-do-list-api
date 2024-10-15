using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

interface ICreateTaskUseCase
{
    Task<Response<CreateTaskResponse>> CreateTask(string description);
}
