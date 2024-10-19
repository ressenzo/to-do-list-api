using ToDoList.Application.Responses;

namespace ToDoList.Test.Builders;

public class CreateTaskResponseBuilder : BaseBuilder<Response<CreateTaskResponse>>
{
    private Response<CreateTaskResponse>? _response;

    public CreateTaskResponseBuilder Success()
    {
        var task = new TaskBuilder()
            .Build();
        var response = CreateTaskResponse.Factory(task);
        _response = Response<CreateTaskResponse>.Success(response);
        return this;
    }

    public CreateTaskResponseBuilder ValidationError()
    {
        _response = Response<CreateTaskResponse>
            .ValidationError([]);
        return this;
    }

    public override Response<CreateTaskResponse> Build() =>
        _response!;
}
