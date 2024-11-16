using ToDoList.Application.Responses;

namespace ToDoList.Application.UseCases.Interfaces;

public interface ISetTaskDoneUseCase
{
    Task<Response<UpdateTaskResponse>> SetTaskDone(string id);
}
