using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface ISetTaskCanceledUseCase
{
    Task<Response<UpdateTaskResponse>> SetTaskCanceled(string id);
}
