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
    ISetTaskInProgressUseCase setTaskInProgressUseCase,
    ISetTaskDoneUseCase setTaskDoneUseCase) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all tasks")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(GetTasksResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(Response))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(Response))]
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
    [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(CreateTaskResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(Response))]
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
    [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(Response))]
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

    [HttpPut("{id}/done")]
    [SwaggerOperation("Set a task as done")]
    [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(Response))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(Response))]
    public async Task<IActionResult> SetTaskDone(string id)
    {
        var result = await setTaskDoneUseCase
            .SetTaskDone(id);

        return result.Type switch
        {
            ResponseType.VALIDATION_ERROR => BadRequest(result),
            ResponseType.SUCCESS => NoContent(),
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
