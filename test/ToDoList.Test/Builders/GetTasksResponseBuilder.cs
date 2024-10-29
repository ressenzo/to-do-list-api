using ToDoList.Application.Responses;
using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Test.Builders;

public class GetTasksResponseBuilder : BaseBuilder<Response<GetTasksResponse>>
{
    private Response<GetTasksResponse>? _response;

    public GetTasksResponseBuilder Success()
    {
        var task = new TaskBuilder()
            .Build();
        var tasks = new ITask[] { task };
        var response = GetTasksResponse
            .Construct(tasks);
        _response = Response<GetTasksResponse>
            .Success(response);
        return this;
    }

    public GetTasksResponseBuilder NotFound()
    {
        _response = Response<GetTasksResponse>
            .NotFound([]);
        return this;
    }

    public GetTasksResponseBuilder InternalError()
    {
        _response = Response<GetTasksResponse>
            .InternalError();
        return this;
    }

    public override Response<GetTasksResponse> Build() =>
        _response!;
}
