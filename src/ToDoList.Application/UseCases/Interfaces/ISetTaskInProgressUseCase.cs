using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface ISetTaskInProgressUseCase
{
    Task<Response<UpdateTaskResponse>> SetTaskInProgress(string id);
}
