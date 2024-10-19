using System.Text.Json.Serialization;

namespace ToDoList.Application.Responses;

public class Response<T> where T : class
{
    private readonly List<string> _errors;

    public T? Content { get; private set; }

    [JsonIgnore]
    public ResponseType Type { get; private set; }

    public IEnumerable<string> Errors => _errors;

    [JsonIgnore]
    public bool IsSuccess => Type == ResponseType.SUCCESS;

    private Response(T content,
        ResponseType type,
        IEnumerable<string> errors)
    {
        _errors = [];
        _errors.AddRange(errors);
        Content = content;
        Type = type;
    }

    public static Response<T> Success(T content) =>
        new(content,
            ResponseType.SUCCESS,
            errors: []);
    
    public static Response<T> ValidationError(
        IEnumerable<string> errors) =>
        new(content: null!,
            ResponseType.VALIDATION_ERROR,
            errors);

    public static Response<T> InternalError() =>
        new(content: null!,
            ResponseType.INTERNAL_ERROR,
            errors: ["An error ocurred during this operation"]);
}

public enum ResponseType
{
    SUCCESS,
    VALIDATION_ERROR,
    INTERNAL_ERROR
}
