using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Application.UseCases;

public class CreateTaskUseCase() : ICreateTaskUseCase
{
    public async Task<Response<CreateTaskResponse>> CreateTask(string description)
    {
        return Response<CreateTaskResponse>.ValidationError([]);
    }
}
