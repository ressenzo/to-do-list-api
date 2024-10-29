using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Application.UseCases;

internal class GetTasksUseCase(
    ILogger<GetTasksUseCase> logger,
    ITaskRepository taskRepository) : IGetTasksUseCase
{
    public async Task<Response<GetTasksResponse>> GetTasks()
    {
        try
        {
            var tasks = await taskRepository.GetTasks();
            if (tasks is null ||
                !tasks.Any())
            {
                return Response<GetTasksResponse>.NotFound("None task was found");
            }
            
            var response = GetTasksResponse.Construct(tasks);
            return Response<GetTasksResponse>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "{Message}",
                ex.Message);
            return Response<GetTasksResponse>.InternalError();
        }
    }
}
