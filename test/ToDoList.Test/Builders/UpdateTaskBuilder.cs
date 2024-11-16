using ToDoList.Application.Responses;

namespace ToDoList.Test.Builders;

public class UpdateTaskBuilder : BaseBuilder<Response<UpdateTaskResponse>>
{
    private Response<UpdateTaskResponse>? _response;

    public UpdateTaskBuilder ValidationError()
    {
        var errors = new List<string> { "Error" };
        _response = Response<UpdateTaskResponse>.ValidationError(errors);
        return this;
    }

    public UpdateTaskBuilder Success()
    {
        var task = new TaskBuilder()
            .Build();
        var response = UpdateTaskResponse
            .Construct(task);
        _response = Response<UpdateTaskResponse>
            .Success(response);
        return this;
    }

    public UpdateTaskBuilder InternalError()
    {
        _response = Response<UpdateTaskResponse>.InternalError();
        return this;
    }

    public override Response<UpdateTaskResponse> Build() =>
        _response!;
}
