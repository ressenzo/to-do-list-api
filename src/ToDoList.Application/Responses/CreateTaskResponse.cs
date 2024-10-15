using ToDoList.Domain.Enums;

namespace ToDoList.Application.Responses;

public record CreateTaskResponse(
    string Id,
    string Description,
    DateTime CreationDate,
    Status Status);
