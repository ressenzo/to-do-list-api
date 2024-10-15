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

    public static ITask Factory(string description) =>
        new Task(description);

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

    public void SetAsInProgress() =>
        Status = Status.IN_PROGRESS;

    public void SetAsDone() =>
        Status = Status.DONE;

    public void SetAsCanceled() =>
        Status = Status.CANCELED;
}
