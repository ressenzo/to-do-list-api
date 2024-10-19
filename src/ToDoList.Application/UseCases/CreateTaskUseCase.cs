using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Domain.Factories.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Application.UseCases;

internal class CreateTaskUseCase(
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
                .Factory(task);
            return Response<CreateTaskResponse>
                .Success(response);
        }
        catch (Exception)
        {
            return Response<CreateTaskResponse>.InternalError();
        }
    }
}
