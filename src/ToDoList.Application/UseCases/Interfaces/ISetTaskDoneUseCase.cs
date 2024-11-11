using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface ISetTaskDoneUseCase
{
    Task<Response> SetTaskDone(string id);
}
