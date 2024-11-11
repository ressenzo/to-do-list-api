using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.Application.Requests;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController(
    IGetTasksUseCase getTasksUseCase,
    ICreateTaskUseCase createTaskUseCase,
    ISetTaskInProgressUseCase setTaskInProgressUseCase) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all tasks")]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.OK, type: typeof(GetTasksResponse))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.NotFound, type: typeof(Response))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(Response))]
    public async Task<IActionResult> GetTasks()
    {
        var result = await getTasksUseCase
            .GetTasks();
        return result.Type switch
        {
            ResponseType.SUCCESS => Ok(result.Content),
            ResponseType.NOT_FOUND => Ok(result.Content),
            ResponseType.INTERNAL_ERROR => ReturnInternalError(result),
            _ => ReturnInternalError(result)
        };
    }

    [HttpPost]
    [SwaggerOperation("Create a new task")]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.Created, type: typeof(CreateTaskResponse))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(Response))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(Response))]
    public async Task<IActionResult> CreateTask(
        CreateTaskRequest request)
    {
        var result = await createTaskUseCase
            .CreateTask(request.Description);
        
        return result.Type switch
        {
            ResponseType.SUCCESS => Created(
                $"api/tasks/{result.Content!.Id}",
                result.Content),
            ResponseType.VALIDATION_ERROR => BadRequest(result),
            ResponseType.INTERNAL_ERROR => ReturnInternalError(result),
            _ => ReturnInternalError(result)
        };
    }

    [HttpPut("{id}/in-progress")]
    [SwaggerOperation("Set a task as in progress")]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.NoContent)]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(Response))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(Response))]
    public async Task<IActionResult> SetTaskInProgress(string id)
    {
        var result = await setTaskInProgressUseCase
            .SetTaskInProgress(id);

        return result.Type switch
        {
            ResponseType.SUCCESS => NoContent(),
            ResponseType.VALIDATION_ERROR => BadRequest(result),
            ResponseType.INTERNAL_ERROR => ReturnInternalError(result),
            _ => ReturnInternalError(result)
        };
    }

    private static ObjectResult ReturnInternalError<T>(Response<T> result) where T : class =>
        new(result)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

    private static ObjectResult ReturnInternalError(Response result) =>
        new(result)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
}
