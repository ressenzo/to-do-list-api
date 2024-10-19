using ToDoList.Application.Requests;

namespace ToDoList.Test.Builders;

public class CreateTaskRequestBuilder : BaseBuilder<CreateTaskRequest>
{
    public override CreateTaskRequest Build() =>
        new("My task");
}
