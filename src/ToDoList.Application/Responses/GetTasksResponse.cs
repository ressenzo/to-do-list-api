using ToDoList.Domain.Entities.Interfaces;

namespace ToDoList.Application.Responses;

public class GetTasksResponse
{
    private GetTasksResponse(IEnumerable<ITask> tasks)
    {
        Tasks = tasks.Select(x => new GetTaskModel(x.Id,
            x.Description,
            x.CreationDate,
            (int)x.Status));
    }
    
    public IEnumerable<GetTaskModel> Tasks { get; private set; }

    public static GetTasksResponse Construct(
        IEnumerable<ITask> tasks) =>
        new(tasks);
}

public record GetTaskModel(string Id,
    string Description,
    DateTime CreationDate,
    int Status);
