using ToDoList.Domain.Enums;
using Task = ToDoList.Domain.Entities.Task;

namespace ToDoList.Test.Builders;

public class TaskBuilder : BaseBuilder<Task>
{
    private Status _status;    

    public TaskBuilder()
    {
        _status = Status.IN_PROGRESS;
    }

    public TaskBuilder WithStatus(Status status)
    {
        _status = status;
        return this;
    }

    public override Task Build()
    {
        var task = Task.Construct(id: "bhj61789",
            description: "My task",
            DateTime.Now,
            _status);
        return (Task)task;
    }
}
