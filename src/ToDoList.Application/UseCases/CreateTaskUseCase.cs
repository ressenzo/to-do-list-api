using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Domain.Factories.Interfaces;

namespace ToDoList.Application.UseCases;

public class CreateTaskUseCase(
    ITaskFactory taskFactory) : ICreateTaskUseCase
{
    public async Task<Response<CreateTaskResponse>> CreateTask(string description)
    {
        var task = taskFactory
            .Factory(description);
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
