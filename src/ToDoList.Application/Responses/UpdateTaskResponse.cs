using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Domain.Enums;

namespace ToDoList.Application.Responses;

public record UpdateTaskResponse
{
    public string Id { get; private set; }

    public string Description { get; private set; }

    public DateTime CreationDate { get; private set; }

    public Status Status { get; private set; }

    private UpdateTaskResponse(string id,
        string description,
        DateTime creationDate,
        Status status)
    {
        Id = id;
        Description = description;
        CreationDate = creationDate;
        Status = status;
    }

    public static UpdateTaskResponse Construct(ITask task) =>
        new(task.Id,
            task.Description,
            task.CreationDate,
            task.Status);
}
