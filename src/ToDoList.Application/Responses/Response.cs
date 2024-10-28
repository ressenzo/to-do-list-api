using System.Text.Json.Serialization;

namespace ToDoList.Application.Responses;

public class Response<T> where T : class
{
    private readonly List<string> _errors;

    [JsonIgnore]
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

    public static Response<T> NotFound(
        IEnumerable<string> errors) =>
        new(content: null!,
            ResponseType.NOT_FOUND,
            errors);
}

public class Response
{
    private readonly List<string> _errors;

    [JsonIgnore]
    public ResponseType Type { get; private set; }

    public IEnumerable<string> Errors => _errors;

    [JsonIgnore]
    public bool IsSuccess => Type == ResponseType.SUCCESS;

    private Response(ResponseType type,
        IEnumerable<string> errors)
    {
        _errors = [];
        _errors.AddRange(errors);
        Type = type;
    }

    public static Response Success() =>
        new(ResponseType.SUCCESS,
            errors: []);
    
    public static Response ValidationError(
        IEnumerable<string> errors) =>
        new(ResponseType.VALIDATION_ERROR,
            errors);

    public static Response InternalError() =>
        new(ResponseType.INTERNAL_ERROR,
            errors: ["An error ocurred during this operation"]);

    public static Response NotFound(
        IEnumerable<string> errors) =>
        new(ResponseType.NOT_FOUND,
            errors);
}

public enum ResponseType
{
    SUCCESS,
    VALIDATION_ERROR,
    INTERNAL_ERROR,
    NOT_FOUND
}
