using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using Task = ToDoList.Domain.Entities.Task;

namespace ToDoList.Application.UseCases;

public class CreateTaskUseCase() : ICreateTaskUseCase
{
    public async Task<Response<CreateTaskResponse>> CreateTask(string description)
    {
        var task = Task.Construct(description);
        if (!task.IsValid())
        {
            return Response<CreateTaskResponse>.ValidationError(
                task.Errors);
        }

        var response = CreateTaskResponse
            .Factory(task);
        return Response<CreateTaskResponse>
            .Success(response);
    }
}
