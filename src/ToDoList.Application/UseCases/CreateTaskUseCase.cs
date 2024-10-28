using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Domain.Factories.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Application.UseCases;

internal class CreateTaskUseCase(
    ILogger<CreateTaskUseCase> logger,
    ITaskFactory taskFactory,
    ITaskRepository taskRepository) : ICreateTaskUseCase
{
    public async Task<Response<CreateTaskResponse>> CreateTask(string description)
    {
        try
        {
            var task = taskFactory
                .Factory(description);
            if (!task.IsValid())
            {
                return Response<CreateTaskResponse>.ValidationError(
                    task.Errors);
            }

            await taskRepository.CreateTask(task);
            var response = CreateTaskResponse
                .Construct(task);
            return Response<CreateTaskResponse>
                .Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "{Message}",
                ex.Message);
            return Response<CreateTaskResponse>.InternalError();
        }
    }
}
