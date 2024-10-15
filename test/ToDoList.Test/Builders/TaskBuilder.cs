using Task = ToDoList.Domain.Entities.Task;

namespace ToDoList.Test.Builders;

public class TaskBuilder : BaseBuilder<Task>
{
    public override Task Build() =>
        (Task)Task.Factory("My task");
}
