using ToDoList.Application.Responses;

namespace ToDoList.Test.Builders;

public class SetTaskStatusBuilder : BaseBuilder<Response>
{
    private Response? _response;

    public SetTaskStatusBuilder ValidationError()
    {
        var errors = new List<string> { "Error" };
        _response = Response.ValidationError(errors);
        return this;
    }

    public SetTaskStatusBuilder Success()
    {
        _response = Response.Success();
        return this;
    }

    public SetTaskStatusBuilder InternalError()
    {
        _response = Response.InternalError();
        return this;
    }

    public override Response Build() =>
        _response!;
}
