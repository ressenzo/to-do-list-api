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

    public async Task<Response<UpdateTaskResponse>> SetTaskInProgress(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id) ||
                id.Length > _ID_LENGTH)
            {
                return Response<UpdateTaskResponse>
                    .ValidationError([$"{nameof(id)} should be passed"]);
            }

            return await GetAndUpdateTask(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "{Message}",
                ex.Message);
            return Response<UpdateTaskResponse>.InternalError();
        }
    }

    private async Task<Response<UpdateTaskResponse>> GetAndUpdateTask(string id)
    {
        var task = await taskRepository.GetTask(id);
        if (task is null)
        {
            return Response<UpdateTaskResponse>
                .NotFound([$"Task with id {id} not found"]);
        }

        task.SetAsInProgress();
        var updateResult = await taskRepository.UpdateTask(task);
        if (updateResult)
        {
            var response = UpdateTaskResponse.Construct(task);
            return Response<UpdateTaskResponse>.Success(response);
        }
        
        return Response<UpdateTaskResponse>.InternalError();
    }
}
