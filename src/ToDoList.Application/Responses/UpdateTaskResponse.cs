using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Application.Responses;

public class UpdateTaskResponse
{
    private UpdateTaskResponse(ITask task)
    {
        Task = new(task.Id,
            task.Description,
            task.CreationDate,
            (int)task.Status);
    }

    public UpdateTaskModel Task { get; set; }

    public static UpdateTaskResponse Construct(
        ITask task) =>
        new(task);
}

public record UpdateTaskModel(string Id,
    string Description,
    DateTime CreationDate,
    int Status);
