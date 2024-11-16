using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface ISetTaskCanceledUseCase
{
    Task<Response> SetTaskCanceled(string id);
}
