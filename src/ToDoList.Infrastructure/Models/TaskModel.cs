using MongoDB.Bson.Serialization.Attributes;
using Task = ToDoList.Domain.Entities.Task;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Domain.Enums;

namespace ToDoList.Infrastructure.Models;

internal class TaskModel
{
    [BsonId]
    public string Id { get; private set; }

    public string Description { get; private set; }

    public DateTime CreationDate { get; private set; }

    public int Status { get; private set; }

    private TaskModel(string id,
        string description,
        DateTime creationDate,
        int status)
    {
        Id = id;
        Description = description;
        CreationDate = creationDate;
        Status = status;
    }

    public static TaskModel Construct(ITask task) =>
        new(task.Id,
            task.Description,
            task.CreationDate,
            (int)task.Status);

    public ITask ToEntity() =>
        Task.Construct(Id,
            Description,
            CreationDate,
            (Status)Status);
}
