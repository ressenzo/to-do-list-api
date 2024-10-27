using ToDoList.Application.Responses;

namespace ToDoList.Test.Builders;

public class SetTaskInProgressResponseBuilder : BaseBuilder<Response>
{
    private Response? _response;

    public SetTaskInProgressResponseBuilder ValidationError()
    {
        var errors = new List<string> { "Error" };
        _response = Response.ValidationError(errors);
        return this;
    }

    public SetTaskInProgressResponseBuilder Success()
    {
        _response = Response.Success();
        return this;
    }

    public SetTaskInProgressResponseBuilder InternalError()
    {
        _response = Response.InternalError();
        return this;
    }

    public override Response Build() =>
        _response!;
}
