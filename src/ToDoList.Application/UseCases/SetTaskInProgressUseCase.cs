using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Application.UseCases;

internal class SetTaskInProgressUseCase(
    ILogger<SetTaskInProgressUseCase> logger,
    ITaskRepository taskRepository) : ISetTaskInProgressUseCase
{
    private const int _ID_LENGTH = 8;

    public async Task<Response> SetTaskInProgress(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id) ||
                id.Length > _ID_LENGTH)
            {
                return Response
                    .ValidationError([$"{nameof(id)} should be passed"]);
            }

            var task = await taskRepository.GetTask(id);
            if (task is null)
            {
                return Response
                    .NotFound([$"Task with id {id} not found"]);
            }

            task.SetAsInProgress();
            var updateResult = await taskRepository.UpdateTask(task);
            if (updateResult)
                return Response.Success();
            
            return Response.InternalError();
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "{Message}",
                ex.Message);
            return Response.InternalError();
        }
    }
}
