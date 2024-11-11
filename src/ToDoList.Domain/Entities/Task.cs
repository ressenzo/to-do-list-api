using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities;

public class Task : Entity, ITask
{
    private const uint _DESCRIPTION_MAX_LENGTH = 100;

    public string Description { get; private set; }

    public DateTime CreationDate { get; private set; }

    public Status Status { get; private set; }

    private Task(string description)
    {
        Description = description;
        CreationDate = DateTime.Now;
        Status = Status.CREATED;
    }

    private Task(string id,
        string description,
        DateTime creationDate,
        Status status) : base(id)
    {
        Description = description;
        CreationDate = creationDate;
        Status = status;
    }

    public static ITask Construct(string description) =>
        new Task(description);

    public static ITask Construct(string id,
        string description,
        DateTime creationDate,
        Status status) =>
        new Task(id,
            description,
            creationDate,
            status);

    public override bool IsValid()
    {
        var isValid = true;
        if (string.IsNullOrWhiteSpace(Description))
        {
            AddError($"{nameof(Description)} should not be empty or null");
            isValid = false;
        }
        else if (Description.Length > _DESCRIPTION_MAX_LENGTH)
        {
            AddError($"{nameof(Description)} length should be less than 100 characters");
            isValid = false;
        }

        return isValid;
    }

    public void SetAsInProgress()
    {
        if (Status.Equals(Status.DONE) ||
            Status.Equals(Status.CANCELED))
            return;

        Status = Status.IN_PROGRESS;
    }

    public void SetAsDone()
    {
        if (Status.Equals(Status.CREATED) ||
            Status.Equals(Status.CANCELED))
            return;

        Status = Status.DONE;
    }

    public void SetAsCanceled()
    {
        if (Status.Equals(Status.DONE))
            return;

        Status = Status.CANCELED;
    }
}
