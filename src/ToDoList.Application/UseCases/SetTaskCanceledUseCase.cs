using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Application.UseCases;

public class SetTaskCanceledUseCase(
    ILogger<SetTaskCanceledUseCase> logger,
    ITaskRepository taskRepository) : ISetTaskCanceledUseCase
{
    private const int _ID_LENGTH = 8;

    public async Task<Response> SetTaskCanceled(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id) ||
            id.Length > _ID_LENGTH)
            {
                return Response
                    .ValidationError([$"{nameof(id)} should be passed"]);
            }

            return await GetTaskAndUpdate(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "{Message}",
                ex.Message);
            return Response.InternalError();
        }
    }

    private async Task<Response> GetTaskAndUpdate(string id)
    {
        var task = await taskRepository
            .GetTask(id);

        if (task is null)
        {
            return Response
                .NotFound([$"Task with id {id} not found"]);
        }

        task.SetAsCanceled();
        var updateResult = await taskRepository
            .UpdateTask(task);

        if (updateResult)
            return Response.Success();

        return Response.InternalError();
    }
}
